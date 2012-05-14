using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AgentMulder.ReSharper.Domain.Utils;
using JetBrains.ProjectModel.Model2.Assemblies.Interfaces;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Resolve;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Util;
using Scully.Metadata;

namespace AgentMulder.ReSharper.Domain.Expressions
{
    public static class WherePredicateBuilder
    {
        public static Expression<Predicate<TReturn>> FromLambda<TReturn>(ILambdaExpression lambdaExpression)
        {
            var visitor = new ExpressionTreeBuilderVisitor<Predicate<TReturn>>();
            
            var result = lambdaExpression.Accept(visitor, null) as Expression<Predicate<TReturn>>;

            return result;
        }

        private class ExpressionTreeBuilderVisitor<TDelegate> : TreeNodeVisitor<IMetadataResolver, Expression>
        {
            private readonly List<ParameterExpression> lambdaParameters = new List<ParameterExpression>(); 

            public override Expression VisitLambdaExpression(ILambdaExpression lambdaExpressionParam, IMetadataResolver context)
            {
                lambdaParameters.AddRange(lambdaExpressionParam.ParameterDeclarations.Select(
                       declaration => declaration.Accept(this, context) as ParameterExpression));

                Expression body = lambdaExpressionParam.BodyExpression.Accept(this, context);
                
                return Expression.Lambda<TDelegate>(body, lambdaParameters);
            }

            public override Expression VisitLambdaParameterDeclaration(ILambdaParameterDeclaration lambdaParameterDeclarationParam, IMetadataResolver context)
            {
                return new ParameterExpressionBuilder(lambdaParameterDeclarationParam, context).Build();
            }

            public override Expression VisitUnaryOperatorExpression(IUnaryOperatorExpression unaryOperatorExpressionParam, IMetadataResolver context)
            {
                Expression operandExpression = unaryOperatorExpressionParam.Operand.Accept(this, context);

                return new UnaryOperatorExpressionBuilder(unaryOperatorExpressionParam, operandExpression).Build();
            }

            public override Expression VisitParenthesizedExpression(IParenthesizedExpression parenthesizedExpressionParam, IMetadataResolver context)
            {
                var binaryExpression = parenthesizedExpressionParam.Expression as IBinaryExpression;
                if (binaryExpression != null)
                {
                    Expression left = binaryExpression.LeftOperand.Accept(this, context);
                    Expression right = binaryExpression.RightOperand.Accept(this, context);
                    return new ConditionalExpressionBuilder(parenthesizedExpressionParam, left, right).Build();
                }

                return base.VisitParenthesizedExpression(parenthesizedExpressionParam, context);
            }

            public override Expression VisitInvocationExpression(IInvocationExpression invocationExpressionParam, IMetadataResolver context)
            {
                return invocationExpressionParam.InvokedExpression.Accept(this, context);
            }

            public override Expression VisitReferenceExpression(IReferenceExpression referenceExpressionParam, IMetadataResolver context)
            {
                if (referenceExpressionParam.QualifierExpression != null)
                {
                    // todo fix, I hate this
                    Expression qualifierExpression = referenceExpressionParam.QualifierExpression.Accept(this, context);

                    var invocationExpression = referenceExpressionParam.Parent as IInvocationExpression;
                    if (invocationExpression != null)
                    {
                        IEnumerable<Expression> arguments = invocationExpression.ArgumentList.Arguments.Select(
                                argument => argument.Accept(this, context));

                        return new MethodCallExpressionBuilder(referenceExpressionParam, qualifierExpression, arguments, context).Build();
                    }

                    return new MemberReferenceExpressionBuilder(referenceExpressionParam, qualifierExpression, context).Build();
                }

                ResolveResultWithInfo resolve = referenceExpressionParam.Reference.Resolve();
                var lambdaParameterDeclaration = resolve.DeclaredElement as ILambdaParameterDeclaration;
                if (lambdaParameterDeclaration != null)
                {
                    return lambdaParameters.First(
                            parameter => parameter.Name.Equals(lambdaParameterDeclaration.NameIdentifier.GetText()));
                }

                var parameterDeclaration = resolve.DeclaredElement as IParameterDeclaration;
                if (parameterDeclaration != null)
                {
                    return new ParameterExpressionBuilder(parameterDeclaration, context).Build();
                }

                return base.VisitReferenceExpression(referenceExpressionParam, context);
            }

            public override Expression VisitCSharpArgument(ICSharpArgument cSharpArgumentParam, IMetadataResolver context)
            {
                return cSharpArgumentParam.Value.Accept(this, context);
            }

            public override Expression VisitEqualityExpression(IEqualityExpression equalityExpressionParam, IMetadataResolver context)
            {
                Expression left = equalityExpressionParam.LeftOperand.Accept(this, context);
                Expression right = equalityExpressionParam.RightOperand.Accept(this, context);
                
                return new EqualityExpressionBuilder(equalityExpressionParam, left, right).Build();
            }

            public override Expression VisitPrimaryExpression(IPrimaryExpression primaryExpressionParam, IMetadataResolver context)
            {
                return null;
            }

            public override Expression VisitTypeofExpression(ITypeofExpression typeofExpressionParam, IMetadataResolver context)
            {
                IDeclaredType declaredType = typeofExpressionParam.ArgumentType.GetScalarType();
                if (declaredType == null || !declaredType.IsValid())
                {
                    return base.VisitTypeofExpression(typeofExpressionParam, context);
                }

                string assemblyName = GetTypeAssemblyFullName(declaredType);
#if DEBUG
                if (string.IsNullOrWhiteSpace(assemblyName))
                {
                    Console.WriteLine(assemblyName);
                }
#endif

                string fullyQualifiedTypeName = string.Format("{0}, {1}", declaredType.GetClrName().FullName, assemblyName);

                Type value = Type.GetType(fullyQualifiedTypeName);

                return Expression.Constant(value, typeof(Type));
            }

            public override Expression VisitUserTypeUsage(IUserTypeUsage userTypeUsageParam, IMetadataResolver context)
            {
                return null;
            }

            public override Expression VisitCSharpLiteralExpression(ICSharpLiteralExpression expression, IMetadataResolver context)
            {
                var declaredType = expression.ConstantValue.Type as IDeclaredType;
                if (declaredType != null)
                {
                    Type type = Type.GetType(declaredType.GetClrName().FullName);
                    Assertion.Assert(type != null, "type != null");
                    return Expression.Constant(expression.ConstantValue.Value, type);
                }

                return Expression.Constant(expression.ConstantValue.Value);
            }

            private string GetTypeAssemblyFullName(IDeclaredType declaredType)
            {
                if (declaredType.Assembly != null)
                {
                    return declaredType.Assembly.FullName;
                }

                IAssembly assembly = declaredType.Module.ContainingProjectModule.GetModuleAssembly();
                if (assembly == null)
                {
                    return null;
                }

                return assembly.FullAssemblyName;
            }
        }

    }
}
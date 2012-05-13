using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using AgentMulder.ReSharper.Domain.Utils;
using JetBrains.ProjectModel.Model2.Assemblies.Interfaces;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Resolve;
using JetBrains.ReSharper.Psi.Resolve;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Util;

namespace AgentMulder.ReSharper.Domain.Expressions
{
    public interface IMetadataResolver
    {

    }

    public static class WherePredicateBuilder
    {
        public static Expression<Predicate<TReturn>> FromLambda<TReturn>(ILambdaExpression lambdaExpression)
        {
            var visitor = new ExpressionTreeBuilderVisitor();
            Expression body = visitor.VisitLambdaExpression(lambdaExpression, null);
            
            return Expression.Lambda<Predicate<TReturn>>(body, Expression.Parameter(typeof(TReturn)));
        }

        private class ExpressionTreeBuilderVisitor : TreeNodeVisitor<IMetadataResolver, Expression>
        {
            public override Expression VisitLambdaExpression(ILambdaExpression lambdaExpressionParam, IMetadataResolver context)
            {
                return lambdaExpressionParam.BodyExpression.Accept(this, context);
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
                    return new ParenthesizedExpressionBuilder(parenthesizedExpressionParam, left, right).Build();
                }

                return base.VisitParenthesizedExpression(parenthesizedExpressionParam, context);
            }

            public override Expression VisitInvocationExpression(IInvocationExpression invocationExpressionParam, IMetadataResolver context)
            {
                return invocationExpressionParam.InvokedExpression.Accept(this, context);
            }

            public override Expression VisitCSharpArgument(ICSharpArgument cSharpArgumentParam, IMetadataResolver context)
            {
                var referenceExpression = cSharpArgumentParam.Value as IReferenceExpression;

                if (referenceExpression != null)
                {
                    ResolveResultWithInfo resolve = referenceExpression.Reference.Resolve();
                    var parameterDeclaration = resolve.DeclaredElement as IParameterDeclaration;
                    if (parameterDeclaration != null)
                    {
                        string parameterName = parameterDeclaration.DeclaredElement.ShortName;
                        IType type = parameterDeclaration.Type;

                        Type reflectionType = type.ToReflectionType();
                    }
                }


                return null;
            }

            public override Expression VisitEqualityExpression(IEqualityExpression equalityExpressionParam, IMetadataResolver context)
            {
                Expression left = equalityExpressionParam.LeftOperand.Accept(this, context);
                Expression right = equalityExpressionParam.RightOperand.Accept(this, context);
                
                return new EqualityExpressionBuilder(equalityExpressionParam, left, right).Build();
            }

            public override Expression VisitReferenceExpression(IReferenceExpression referenceExpressionParam, IMetadataResolver context)
            {
                if (referenceExpressionParam.QualifierExpression != null)
                {
                    Expression expression = referenceExpressionParam.QualifierExpression.Accept(this, context);
                    
                    return new MemberExpressionBuilder(referenceExpressionParam, expression, context).Build();
                }

                ResolveResultWithInfo resolve = referenceExpressionParam.Reference.Resolve(); //todo check resolve result, might not be ok
                var parameterDeclaration = resolve.DeclaredElement as IParameterDeclaration;
                if (parameterDeclaration != null)
                {
                    return new ParameterExpressionBuilder(parameterDeclaration, context).Build();
                }
                
                return base.VisitReferenceExpression(referenceExpressionParam, context);
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
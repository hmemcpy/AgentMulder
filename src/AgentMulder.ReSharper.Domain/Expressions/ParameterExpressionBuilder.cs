using System;
using System.Linq.Expressions;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.ReSharper.Domain.Expressions
{
    internal class ParameterExpressionBuilder : ExpressionBuilder<IParameterDeclaration>
    {
        private readonly IParameterDeclaration declaration;
        private readonly IMetadataResolver metadataResolver;

        public ParameterExpressionBuilder(IParameterDeclaration declaration, IMetadataResolver metadataResolver)
        {
            this.declaration = declaration;
            this.metadataResolver = metadataResolver;
        }

        public override Expression Build()
        {
            Type parameterType = GetParameterType(declaration.Type);

            return Expression.Parameter(parameterType, declaration.DeclaredName);
        }

        private Type GetParameterType(IType type)
        {
            IDeclaredType declaredType = type.GetScalarType();
            if (declaredType != null)
            {
                return Type.GetType(declaredType.GetClrName().FullName);
            }

            // todo temp hack
            return typeof(Type);
        }
    }
}
using System;
using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace AgentMulder.ReSharper.Domain
{
    public static class WherePredicateBuilder
    {
        public static Predicate<T> FromLambda<T>(ILambdaExpression lambdaExpression)
        {
            ILambdaParameterDeclaration parameterDeclaration = lambdaExpression.ParameterDeclarations[0];
            
            return t => false;
        }
    }
}
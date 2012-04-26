using System.Collections.Generic;
using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace AgentMulder.ReSharper.Domain
{
    public static class Extensions
    {
        public static IEnumerable<IInvocationExpression> GetAllExpressions(this IInvocationExpression expression)
        {
            for (var e = expression; e != null; e = ((IReferenceExpression)e.InvokedExpression).QualifierExpression as IInvocationExpression)
                yield return e;
        }             
    }
}
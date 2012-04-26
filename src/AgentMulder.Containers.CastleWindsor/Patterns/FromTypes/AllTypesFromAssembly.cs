using System;
using System.Collections.Generic;
using AgentMulder.ReSharper.Domain.Registrations;
using AgentMulder.ReSharper.Domain.Search;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.CastleWindsor.Patterns.FromTypes
{
    internal class AllTypesFromAssembly : RegistrationBase
    {
        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$alltypes$.FromAssembly($assembly$)",
                new ExpressionPlaceholder("alltypes", "Castle.MicroKernel.Registration.AllTypes"),
                new ArgumentPlaceholder("assembly", 1, 1)); // matches exactly one argument
        
        public AllTypesFromAssembly()
            : base(pattern)
        {
        }

        public override IEnumerable<IComponentRegistration>  GetComponentRegistrations(ITreeNode parentElement)
        {
            IInvocationExpression invocationExpression = GetMatchedExpression(parentElement);
            IStructuralMatcher matcher = CreateMatcher();
            IStructuralMatchResult match = matcher.Match(invocationExpression);
            if (match.Matched)
            {
                var argument = (ICSharpArgument)match.GetMatchedElement("assembly");
                var invocation = argument.Value as IInvocationExpression;
                if (invocation != null)
                {
                    string dump = invocation.InvocationExpressionReference.Resolve().Result.Dump();
                }
            }
            yield break;
        }
    }
}
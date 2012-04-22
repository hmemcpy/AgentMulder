using System;
using System.Collections.Generic;
using AgentMulder.ReSharper.Domain.Registrations;
using AgentMulder.ReSharper.Domain.Search;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.Containers.CastleWindsor.Patterns.FromTypes
{
    internal class AllTypesFromAssembly : RegistrationBase
    {
        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$alltypes$.FromAssembly($assembly$)",
                new ExpressionPlaceholder("alltypes", "Castle.MicroKernel.Registration.AllTypes"),
                new ArgumentPlaceholder("assembly", 1, 1));
        
        public AllTypesFromAssembly()
            : base(pattern)
        {
        }

        public override IComponentRegistrationCreator CreateComponentRegistrationCreator()
        {
            return new FromAssemblyCreator();
        }

        private sealed class FromAssemblyCreator : IComponentRegistrationCreator
        {
            public IEnumerable<IComponentRegistration> CreateRegistrations(params IStructuralMatchResult[] matchResults)
            {
                foreach (var match in matchResults)
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
}
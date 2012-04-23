using System;
using System.Collections.Generic;
using System.Linq;
using AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.BasedOn;
using AgentMulder.ReSharper.Domain.Registrations;
using AgentMulder.ReSharper.Domain.Search;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.Containers.CastleWindsor.Patterns.FromTypes
{
    internal sealed class AllTypesFromThisAssembly : RegistrationBase
    {
        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$alltypes$.FromThisAssembly()",
                new ExpressionPlaceholder("alltypes", "Castle.MicroKernel.Registration.AllTypes"));

        private readonly BasedOnRegistrationBase basedOnGeneric = new BasedOnGeneric();
        
        public AllTypesFromThisAssembly()
            : base(pattern)
        {
        }

        public override IComponentRegistrationCreator CreateComponentRegistrationCreator()
        {
            return new FromThisAssemblyCreator(this);
        }

        private sealed class FromThisAssemblyCreator : IComponentRegistrationCreator
        {
            private readonly AllTypesFromThisAssembly parent;

            public FromThisAssemblyCreator(AllTypesFromThisAssembly parent)
            {
                this.parent = parent;
            }

            public IEnumerable<IComponentRegistration> CreateRegistrations(params IStructuralMatchResult[] matchResults)
            {
                IComponentRegistrationCreator basedOnCreator = parent.basedOnGeneric.CreateComponentRegistrationCreator();
                IStructuralMatcher basedOnMatcher = parent.basedOnGeneric.CreateMatcher();

                foreach (var match in matchResults)
                {
                    IModule module = match.MatchedElement.GetPsiModule().ContainingProjectModule;

                    IStructuralMatchResult basedOnResult = basedOnMatcher.Match(match.MatchedElement.Parent.Parent);
                    if (basedOnResult.Matched)
                    {
                        IEnumerable<BasedOnRegistration> basedOnRegistrations = basedOnCreator.CreateRegistrations(basedOnResult).OfType<BasedOnRegistration>();

                        foreach (var registration in basedOnRegistrations)
                        {
                            yield return new ModuleBasedOnRegistration(module, registration);
                        }
                    }
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.BasedOn;
using AgentMulder.ReSharper.Domain.Registrations;
using AgentMulder.ReSharper.Domain.Search;
using JetBrains.ProjectModel;
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

        public override IEnumerable<IComponentRegistration> GetComponentRegistrations(params IStructuralMatchResult[] matchResults)
        {
            IStructuralMatcher basedOnMatcher = basedOnGeneric.CreateMatcher();

            foreach (var match in matchResults)
            {
                IModule module = match.MatchedElement.GetPsiModule().ContainingProjectModule;

                IStructuralMatchResult basedOnResult = basedOnMatcher.Match(match.MatchedElement.Parent.Parent);
                if (basedOnResult.Matched)
                {
                    IEnumerable<BasedOnRegistration> basedOnRegistrations = basedOnGeneric.GetComponentRegistrations(basedOnResult).OfType<BasedOnRegistration>();

                    foreach (var registration in basedOnRegistrations)
                    {
                        yield return new ModuleBasedOnRegistration(module, registration);
                    }
                }
            }
        }
    }
}
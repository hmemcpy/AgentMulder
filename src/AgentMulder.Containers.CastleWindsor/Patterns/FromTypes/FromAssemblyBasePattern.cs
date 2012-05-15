using System;
using System.Collections.Generic;
using System.Linq;
using AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.BasedOn;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.CastleWindsor.Patterns.FromTypes
{
    public abstract class FromAssemblyBasePattern : FromTypesBasePattern
    {
        protected FromAssemblyBasePattern(IStructuralSearchPattern pattern, params BasedOnRegistrationBasePattern[] basedOnPatterns)
            : base(pattern, basedOnPatterns)
        {
        }

        public override IEnumerable<IComponentRegistration> GetComponentRegistrations(ITreeNode registrationRootElement)
        {
            IStructuralMatchResult match = Match(registrationRootElement);

            if (match.Matched)
            {
                IModule module = GetTargetModule(match);

                foreach (var basedOnPattern in BasedOnPatterns)
                {
                    var basedOnRegistrations = basedOnPattern.GetComponentRegistrations(registrationRootElement).Cast<BasedOnRegistrationBase>();

                    foreach (var registration in basedOnRegistrations)
                    {
                        registration.AddFilter(Filter);

                        yield return new ModuleBasedOnRegistration(module, registration);
                    }
                }
            }
        }

        protected abstract IModule GetTargetModule(IStructuralMatchResult match);
    }
}
using System.Collections.Generic;
using System.Linq;
using AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.BasedOn;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.CastleWindsor.Patterns.FromTypes
{
    public abstract class FromAssemblyBase : FromTypesBase
    {
        protected FromAssemblyBase(IStructuralSearchPattern pattern, params BasedOnRegistrationBase[] basedOnPatterns)
            : base(pattern, basedOnPatterns)
        {
        }

        public override IEnumerable<IComponentRegistration> GetComponentRegistrations(ITreeNode parentElement)
        {
            IStructuralMatchResult match = Match(parentElement);

            if (match.Matched)
            {
                IModule module = GetTargetModule(match);

                foreach (var basedOnPattern in basedOnPatterns)
                {
                    var basedOnRegistrations = basedOnPattern.GetComponentRegistrations(parentElement).OfType<BasedOnRegistration>();

                    foreach (var registration in basedOnRegistrations)
                    {
                        yield return new ModuleBasedOnRegistration(module, registration);
                    }
                }
            }
        }

        protected abstract IModule GetTargetModule(IStructuralMatchResult match);
    }
}
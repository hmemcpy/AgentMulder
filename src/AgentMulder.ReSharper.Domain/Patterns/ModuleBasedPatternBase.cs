using System.Collections.Generic;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.ReSharper.Domain.Patterns
{
    public abstract class ModuleBasedPatternBase : RegistrationPatternBase
    {
        protected ModuleBasedPatternBase(IStructuralSearchPattern pattern)
            : base(pattern)
        {
        }

        public override IEnumerable<IComponentRegistration> GetComponentRegistrations(ITreeNode registrationRootElement)
        {
            IStructuralMatchResult match = Match(registrationRootElement);

            if (match.Matched)
            {
                IModule module = GetTargetModule(match);

                if (module != null)
                {
                    yield return new ModuleBasedOnRegistration(registrationRootElement, module);
                }
            }
        }

        protected abstract IModule GetTargetModule(IStructuralMatchResult match);
    }
}
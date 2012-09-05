using System.Collections.Generic;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.ReSharper.Domain.Patterns
{
    public abstract class FromAssemblyPatternBase : FromDescriptorPatternBase
    {
        protected FromAssemblyPatternBase(IStructuralSearchPattern pattern, params IBasedOnPattern[] basedOnPatterns)
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
                    var basedOnRegistrations = basedOnPattern.GetBasedOnRegistrations(registrationRootElement);

                    foreach (BasedOnRegistrationBase registration in basedOnRegistrations)
                    {
                        yield return new ModuleBasedOnRegistration(module, registration);
                    }
                }
            }
        }

        protected abstract IModule GetTargetModule(IStructuralMatchResult match);
    }
}
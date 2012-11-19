using System.Collections.Generic;
using System.Linq;
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

                var registrations = (from pattern in BasedOnPatterns
                                     from registration in pattern.GetBasedOnRegistrations(registrationRootElement)
                                     select registration).ToList();

                if (!registrations.Any())
                {
                    yield break;
                }

                yield return new CompositeRegistration(registrationRootElement, registrations.Union(new ComponentRegistrationBase[]
                {
                    new ModuleBasedOnRegistration(registrationRootElement, module)
                }));
            }
        }

        protected abstract IModule GetTargetModule(IStructuralMatchResult match);
    }
}
using System.Collections.Generic;
using AgentMulder.ReSharper.Domain.Patterns;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.CastleWindsor.Patterns.FromTypes
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

                    foreach (FilteredRegistrationBase registration in basedOnRegistrations)
                    {
                        var moduleBasedRegistration = new ModuleBasedOnRegistration(registrationRootElement, module);

                        yield return new CompositeRegistration(registrationRootElement, new ComponentRegistrationBase[] { registration, moduleBasedRegistration });
                    }
                }
            }
        }

        protected abstract IModule GetTargetModule(IStructuralMatchResult match);
    }
}
using System.Collections.Generic;
using System.Linq;
using AgentMulder.ReSharper.Domain.Patterns;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.CastleWindsor.Patterns.Component.ComponentFor
{
    internal abstract class ComponentForBasePattern : ComponentRegistrationPatternBase
    {
        private readonly IEnumerable<ComponentImplementationPatternBase> implementedByPatterns;

        protected ComponentForBasePattern(IStructuralSearchPattern pattern, string elementName, IEnumerable<ComponentImplementationPatternBase> implementedByPatterns) 
            : base(pattern, elementName)
        {
            this.implementedByPatterns = implementedByPatterns;
        }

        public override IEnumerable<IComponentRegistration> GetComponentRegistrations(ITreeNode registrationRootElement)
        {
            foreach (var registration in DoCreateRegistrations(registrationRootElement).OfType<ComponentRegistration>())
            {
                foreach (var implementedByPattern in implementedByPatterns)
                {
                    var implementedByRegistration = implementedByPattern.GetComponentRegistrations(registrationRootElement)
                        .Cast<ComponentRegistration>()
                        .FirstOrDefault();

                    if (implementedByRegistration != null)
                    {
                        registration.Implementation = implementedByRegistration.ServiceType;
                        break;
                    }
                }

                yield return registration;
            }
        }

        protected virtual IEnumerable<IComponentRegistration> DoCreateRegistrations(ITreeNode parentElement)
        {
            return base.GetComponentRegistrations(parentElement);
        }
    }
}
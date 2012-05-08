using System.Collections.Generic;
using System.Linq;
using AgentMulder.Containers.CastleWindsor.Patterns.Component.ImplementedBy;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.CastleWindsor.Patterns.Component.ComponentFor
{
    internal abstract class ComponentForBasePattern : ComponentRegistrationBasePattern
    {
        private readonly IEnumerable<ImplementedByBasePattern> implementedByPatterns;

        protected ComponentForBasePattern(IStructuralSearchPattern pattern, string elementName, IEnumerable<ImplementedByBasePattern> implementedByPatterns) 
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
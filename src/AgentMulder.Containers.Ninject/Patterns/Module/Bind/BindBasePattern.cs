using System.Collections.Generic;
using System.Linq;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.Ninject.Patterns.Module.Bind
{
    internal abstract class BindBasePattern : ComponentRegistrationBasePattern
    {
        private readonly IEnumerable<ComponentImplementationBasePattern> toPatterns;

        protected BindBasePattern(IStructuralSearchPattern pattern, string elementName, IEnumerable<ComponentImplementationBasePattern> toPatterns)
            : base(pattern, elementName)
        {
            this.toPatterns = toPatterns;
        }

        public override IEnumerable<IComponentRegistration> GetComponentRegistrations(ITreeNode registrationRootElement)
        {
            foreach (var registration in DoCreateRegistrations(registrationRootElement).OfType<ComponentRegistration>())
            {
                foreach (var toPattern in toPatterns)
                {
                    var implementedByRegistration = toPattern.GetComponentRegistrations(registrationRootElement)
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
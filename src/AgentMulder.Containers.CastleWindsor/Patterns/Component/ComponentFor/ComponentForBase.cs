using System.Collections.Generic;
using System.Linq;
using AgentMulder.Containers.CastleWindsor.Patterns.Component.ImplementedBy;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.CastleWindsor.Patterns.Component.ComponentFor
{
    internal abstract class ComponentForBase : ComponentRegistrationBase
    {
        private readonly IEnumerable<ImplementedByBase> implementedByPatterns;

        protected ComponentForBase(IStructuralSearchPattern pattern, string elementName, IEnumerable<ImplementedByBase> implementedByPatterns) 
            : base(pattern, elementName)
        {
            this.implementedByPatterns = implementedByPatterns;
        }

        public override IEnumerable<IComponentRegistration> GetComponentRegistrations(ITreeNode parentElement)
        {
            foreach (var registration in DoCreateRegistrations(parentElement).OfType<ComponentRegistration>())
            {
                foreach (var implementedByPattern in implementedByPatterns)
                {
                    var implementedByRegistration = implementedByPattern.GetComponentRegistrations(parentElement)
                        .Cast<ComponentRegistration>().FirstOrDefault();
                    if (implementedByRegistration != null)
                    {
                        registration.Implementation = implementedByRegistration.ServiceType;

                        yield return registration;
                    }
                }
            }
        }

        protected virtual IEnumerable<IComponentRegistration> DoCreateRegistrations(ITreeNode parentElement)
        {
            return base.GetComponentRegistrations(parentElement);
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using AgentMulder.ReSharper.Domain.Patterns;
using AgentMulder.ReSharper.Domain.Registrations;
using AgentMulder.ReSharper.Domain.Utils;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.StructureMap.Patterns.For
{
    internal abstract class ForBasePattern : ComponentRegistrationPatternBase
    {
        private readonly IEnumerable<ComponentImplementationPatternBase> usePatterns;

        protected ForBasePattern(IStructuralSearchPattern pattern, string elementName, IEnumerable<ComponentImplementationPatternBase> usePatterns)
            : base(pattern, elementName)
        {
            this.usePatterns = usePatterns;
        }

        public override IEnumerable<IComponentRegistration> GetComponentRegistrations(ITreeNode registrationRootElement)
        {
            IExpressionStatement statement = registrationRootElement.GetParentExpressionStatement();
            if (statement == null)
            {
                yield break;
            }

            foreach (var usePattern in usePatterns)
            {
                var implementedByRegistration = usePattern.GetComponentRegistrations(statement.Expression)
                    .Cast<ComponentRegistration>()
                    .FirstOrDefault();

                if (implementedByRegistration != null)
                {
                    foreach (var registration in DoCreateRegistrations(statement.Expression).OfType<ComponentRegistration>())
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
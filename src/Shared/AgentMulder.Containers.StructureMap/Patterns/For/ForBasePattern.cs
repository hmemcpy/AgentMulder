using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using AgentMulder.ReSharper.Domain.Patterns;
using AgentMulder.ReSharper.Domain.Registrations;
using AgentMulder.ReSharper.Domain.Utils;
using JetBrains.ReSharper.Psi.CSharp.Tree;
#if SDK90
using JetBrains.ReSharper.Feature.Services.StructuralSearch;
#else
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
#endif
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.StructureMap.Patterns.For
{
    [InheritedExport("ComponentRegistration", typeof(IRegistrationPattern))]
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
            // todo this is duplicated from Ninject!
            // This entire thing is one big hack. Need to come back to it one day :)
            // There is (currently) no way to create a pattern that would match the For() call with implicit 'this' in ReSharper SSR
            // (i.e. a call to Foo being made withing a method, located in the Registry class)
            // Therefore I'm only matching by the method name only, and later verifying that the method indeed belongs to StructureMap, by
            // making sure the invocation's qualifier derived from global::StructureMap.Configuration.DSL.IRegistry

            if (!registrationRootElement.IsContainerCall(Constants.StructureMapRegistryTypeName))
            {
                yield break;
            }

            IInvocationExpression invocationExpression = registrationRootElement.GetInvocationExpression();
            if (invocationExpression == null)
            {
                yield break;
            }

            foreach (var usePattern in usePatterns)
            {
                var implementedByRegistration = usePattern.GetComponentRegistrations(invocationExpression)
                    .Cast<ComponentRegistration>()
                    .FirstOrDefault();

                if (implementedByRegistration != null)
                {
                    foreach (var registration in DoCreateRegistrations(invocationExpression).OfType<ComponentRegistration>())
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
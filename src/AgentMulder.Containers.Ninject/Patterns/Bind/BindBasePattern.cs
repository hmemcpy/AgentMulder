using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using AgentMulder.ReSharper.Domain.Patterns;
using AgentMulder.ReSharper.Domain.Registrations;
using AgentMulder.ReSharper.Domain.Utils;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Feature.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.Ninject.Patterns.Bind
{
    [InheritedExport("ComponentRegistration", typeof(IRegistrationPattern))]
    internal abstract class BindBasePattern : ComponentRegistrationPatternBase
    {
        private const string NinjectBindingRootClrTypeName = "Ninject.Syntax.IBindingRoot";
        private readonly IEnumerable<ComponentRegistrationPatternBase> toPatterns;

        protected BindBasePattern(IStructuralSearchPattern pattern, string elementName, IEnumerable<ComponentRegistrationPatternBase> toPatterns)
            : base(pattern, elementName)
        {
            this.toPatterns = toPatterns;
        }

        public override IEnumerable<IComponentRegistration> GetComponentRegistrations(ITreeNode registrationRootElement)
        {
            // This entire thing is one big hack. Need to come back to it one day :)
            // There is (currently) no way to create a pattern that would match the Bind() call with implicit 'this' in ReSharper SSR.
            // Therefore I'm only matching by the method name only, and later verifying that the method indeed belongs to Ninject, by
            // making sure the invocation's qualifier derived from global::Ninject.Syntax.IBindingRoot

            if (!registrationRootElement.IsContainerCall(NinjectBindingRootClrTypeName))
            {
                yield break;
            }

            var statement = registrationRootElement.GetParentExpression<IExpressionStatement>();
            if (statement == null)
            {
                yield break;
            }

            foreach (var toPattern in toPatterns)
            {
                var implementedByRegistration = toPattern.GetComponentRegistrations(statement.Expression)
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
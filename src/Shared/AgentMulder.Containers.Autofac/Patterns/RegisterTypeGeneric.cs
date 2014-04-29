using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using AgentMulder.ReSharper.Domain.Patterns;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.Autofac.Patterns
{
    [Export("ComponentRegistration", typeof(IRegistrationPattern))]
    internal sealed class RegisterTypeGeneric : ComponentImplementationPatternBase
    {
        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$builder$.RegisterType<$type$>()",
                new ExpressionPlaceholder("builder", "global::Autofac.ContainerBuilder", true),
                new TypePlaceholder("type"));

        public RegisterTypeGeneric()
            : base(pattern, "type")
        {
        }

        public override IEnumerable<IComponentRegistration> GetComponentRegistrations(ITreeNode registrationRootElement)
        {
            foreach (var registration in base.GetComponentRegistrations(registrationRootElement).Cast<ComponentRegistration>())
            {
                registration.Implementation = registration.ServiceType;

                yield return registration;
            }
        }
    }
}
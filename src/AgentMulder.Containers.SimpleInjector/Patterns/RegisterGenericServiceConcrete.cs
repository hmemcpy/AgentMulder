using System.Collections.Generic;
using System.ComponentModel.Composition;
using AgentMulder.ReSharper.Domain.Patterns;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.SimpleInjector.Patterns
{
    [Export("ComponentRegistration", typeof(IRegistrationPattern))]
    public class RegisterGenericServiceConcrete : RegistrationPatternBase
    {
        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$container$.Register<$service$, $concrete$>()",
                new ExpressionPlaceholder("container", "global::SimpleInjector.Container", true),
                new TypePlaceholder("service"),
                new TypePlaceholder("concrete"));

        public RegisterGenericServiceConcrete()
            : base(pattern)
        {
        }

        public override IEnumerable<IComponentRegistration> GetComponentRegistrations(ITreeNode registrationRootElement)
        {
            IStructuralMatchResult match = Match(registrationRootElement);
            
            if (match.Matched)
            {
                var serviceType = match.GetMatchedType("service") as IDeclaredType;
                var concreteType = match.GetMatchedType("concrete") as IDeclaredType;
                if (serviceType != null && concreteType != null)
                {
                    ITypeElement typeElement = serviceType.GetTypeElement();
                    ITypeElement concreteElement = concreteType.GetTypeElement();
                    if (typeElement != null && concreteElement != null) // can be null in case of broken reference (unresolved type)
                    {
                        yield return new ComponentRegistration(registrationRootElement, typeElement, concreteElement);
                    }
                }
            }
        }
    }
}
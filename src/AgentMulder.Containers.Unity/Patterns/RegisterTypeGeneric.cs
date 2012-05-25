using System.Collections.Generic;
using AgentMulder.ReSharper.Domain.Patterns;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.Unity.Patterns
{
    internal sealed class RegisterTypeGeneric : ComponentRegistrationPatternBase
    {
        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$container$.RegisterType<$service$, $type$>($arguments$)",
                new ExpressionPlaceholder("container", "Microsoft.Practices.Unity.IUnityContainer", false),
                new TypePlaceholder("service"),
                new TypePlaceholder("type"),
                new ArgumentPlaceholder("arguments", -1, -1));

        public RegisterTypeGeneric()
            : base(pattern, "")
        {
        }

        public override IEnumerable<IComponentRegistration> GetComponentRegistrations(ITreeNode registrationRootElement)
        {
            IStructuralMatchResult match = Match(registrationRootElement);

            if (match.Matched)
            {
                var service = match.GetMatchedType("service") as IDeclaredType;
                var type = match.GetMatchedType("type") as IDeclaredType;
                if (service == null || type == null)
                {
                    yield break;
                }

                var serviceTypeElement = service.GetTypeElement();
                var typeTypeElement = type.GetTypeElement();
                if (serviceTypeElement == null || typeTypeElement == null)
                {
                    yield break;
                }

                yield return new ComponentRegistration(registrationRootElement, serviceTypeElement)
                {
                    Implementation = typeTypeElement
                };
            }
        }
    }
}
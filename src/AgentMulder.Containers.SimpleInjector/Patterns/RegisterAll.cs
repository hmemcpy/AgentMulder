using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using AgentMulder.ReSharper.Domain.Patterns;
using AgentMulder.ReSharper.Domain.Registrations;
using AgentMulder.ReSharper.Domain.Utils;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.Containers.SimpleInjector.Patterns
{
    [Export("ComponentRegistration", typeof(IRegistrationPattern))]
    public class RegisterAll : RegisterWithService
    {
        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$container$.RegisterAll($arguments$)",
                new ExpressionPlaceholder("container", "global::SimpleInjector.Container", true),
                new ArgumentPlaceholder("arguments", -1, -1));

        public RegisterAll()
            : base(pattern)
        {
        }

        protected override IEnumerable<IComponentRegistration> FromGenericArguments(IInvocationExpression invocationExpression)
        {
            var serviceType = invocationExpression.TypeArguments.First() as IDeclaredType;
            if (serviceType == null)
            {
                yield break;
            }

            var typeElement = serviceType.GetTypeElement();
            if (typeElement == null)
            {
                yield break;
            }

            IEnumerable<ITypeElement> typeElements = invocationExpression.Arguments.SelectMany(argument => argument.Value.GetRegisteredTypes());

            yield return new TypesBasedOnRegistration(typeElements, new ServiceRegistration(invocationExpression, typeElement));
        }

        protected override IEnumerable<IComponentRegistration> FromArguments(IInvocationExpression invocationExpression)
        {
            yield break;
        }
    }
}
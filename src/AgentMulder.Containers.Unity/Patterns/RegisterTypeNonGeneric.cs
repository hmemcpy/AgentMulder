using System.Collections.Generic;
using AgentMulder.ReSharper.Domain.Patterns;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.Unity.Patterns
{
    internal sealed class RegisterTypeNonGeneric : ComponentRegistrationPatternBase
    {
        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$container$.RegisterType($fromType$, $toType$, $otherArgs$)",
                new ExpressionPlaceholder("container", "Microsoft.Practices.Unity.IUnityContainer", false),
                new ArgumentPlaceholder("fromType"),
                new ArgumentPlaceholder("toType"),
                new ArgumentPlaceholder("otherArgs", -1, -1));

        public RegisterTypeNonGeneric()
            : base(pattern, "")
        {
        }

        public override IEnumerable<IComponentRegistration> GetComponentRegistrations(ITreeNode registrationRootElement)
        {
            IStructuralMatchResult match = Match(registrationRootElement);

            if (match.Matched)
            {
                // ReSharper SSR will match a generic method with this pattern too. If has generic arguments, return
                var invocationExpression = match.MatchedElement as IInvocationExpression;
                if (invocationExpression == null || invocationExpression.TypeArguments.Count > 0)
                {
                    yield break;
                }

                ITypeElement fromTypeElement = GetArgumentType(match, "fromType");
                ITypeElement toTypeElement = GetArgumentType(match, "toType");

                if (fromTypeElement == null || toTypeElement == null)
                {
                    yield break;
                }

                yield return new ComponentRegistration(registrationRootElement, fromTypeElement)
                {
                    Implementation = toTypeElement
                };
            }
        }

        private ITypeElement GetArgumentType(IStructuralMatchResult match, string elementName)
        {
            var argument = match.GetMatchedElement(elementName) as ICSharpArgument;
            if (argument == null)
            {
                return null;
            }

            // todo support non-typeof arguments too
            var typeOfExpression = argument.Value as ITypeofExpression;
            if (typeOfExpression == null)
            {
                return null;
            }

            var typeDeclaration = typeOfExpression.ArgumentType as IDeclaredType;
            if (typeDeclaration == null)
            {
                return null;
            }

            return typeDeclaration.GetTypeElement();
        }
    }
}
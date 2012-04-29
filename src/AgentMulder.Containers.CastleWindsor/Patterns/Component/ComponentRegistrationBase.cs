using System.Collections.Generic;
using AgentMulder.ReSharper.Domain.Registrations;
using AgentMulder.ReSharper.Domain.Search;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.CastleWindsor.Patterns.Component
{
    public abstract class ComponentRegistrationBase : RegistrationBase
    {
        private readonly string elementName;

        protected ComponentRegistrationBase(IStructuralSearchPattern pattern, string elementName)
            : base(pattern)
        {
            this.elementName = elementName;
        }

        public override IEnumerable<IComponentRegistration> GetComponentRegistrations(ITreeNode parentElement)
        {
            IInvocationExpression invocationExpression = GetMatchedExpression(parentElement);
            IStructuralMatcher matcher = CreateMatcher();
            IStructuralMatchResult match = matcher.Match(invocationExpression);

            if (match.Matched)
            {
                var matchedType = match.GetMatchedType(elementName) as IDeclaredType;
                if (matchedType != null)
                {
                    ITypeElement typeElement = matchedType.GetTypeElement(match.MatchedElement.GetPsiModule());
                    yield return new ConcreteRegistration(match.GetDocumentRange(), typeElement);
                }
            }
        }
    }
}
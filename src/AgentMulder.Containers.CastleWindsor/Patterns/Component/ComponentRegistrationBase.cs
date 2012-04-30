using System.Collections.Generic;
using AgentMulder.ReSharper.Domain.Registrations;
using AgentMulder.ReSharper.Domain.Search;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.CastleWindsor.Patterns.Component
{
    public abstract class ComponentRegistrationBase : RegistrationBase
    {
        private readonly string elementName;

        protected string ElementName
        {
            get { return elementName; }
        }

        protected ComponentRegistrationBase(IStructuralSearchPattern pattern, string elementName)
            : base(pattern)
        {
            this.elementName = elementName;
        }

        public override IEnumerable<IComponentRegistration> GetComponentRegistrations(ITreeNode parentElement)
        {
            IStructuralMatchResult match = Match(parentElement);
            
            if (match.Matched)
            {
                var matchedType = match.GetMatchedType(elementName) as IDeclaredType;
                if (matchedType != null)
                {
                    ITypeElement typeElement = matchedType.GetTypeElement(match.MatchedElement.GetPsiModule());
                    yield return new ComponentRegistration(match.GetDocumentRange(), typeElement);
                }
            }
        }
    }
}
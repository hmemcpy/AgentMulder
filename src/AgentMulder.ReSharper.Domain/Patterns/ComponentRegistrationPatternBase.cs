using System.Collections.Generic;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Feature.Services.StructuralSearch;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.ReSharper.Domain.Patterns
{
    public abstract class ComponentRegistrationPatternBase : RegistrationPatternBase
    {
        private readonly string elementName;

        protected string ElementName
        {
            get { return elementName; }
        }

        protected ComponentRegistrationPatternBase(IStructuralSearchPattern pattern, string elementName)
            : base(pattern)
        {
            this.elementName = elementName;
        }

        public override IEnumerable<IComponentRegistration> GetComponentRegistrations(ITreeNode registrationRootElement)
        {
            IStructuralMatchResult match = Match(registrationRootElement);
            
            if (match.Matched)
            {
                var matchedType = match.GetMatchedType(elementName) as IDeclaredType;
                if (matchedType != null)
                {
                    ITypeElement typeElement = matchedType.GetTypeElement();
                    if (typeElement != null) // can be null in case of broken reference (unresolved type)
                    {
                        yield return new ComponentRegistration(registrationRootElement, typeElement);
                    }
                }
            }
        }
    }
}
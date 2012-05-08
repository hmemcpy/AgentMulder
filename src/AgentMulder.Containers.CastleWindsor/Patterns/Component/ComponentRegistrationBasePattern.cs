using System.Collections.Generic;
using AgentMulder.ReSharper.Domain.Registrations;
using AgentMulder.ReSharper.Domain.Search;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.CastleWindsor.Patterns.Component
{
    public abstract class ComponentRegistrationBasePattern : RegistrationBasePattern
    {
        private readonly string elementName;

        protected string ElementName
        {
            get { return elementName; }
        }

        protected ComponentRegistrationBasePattern(IStructuralSearchPattern pattern, string elementName)
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
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using AgentMulder.ReSharper.Domain.Registrations;
using AgentMulder.ReSharper.Domain.Search;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.Containers.CastleWindsor.Patterns.Component
{
    [PartNotDiscoverable]
    public abstract class ManualRegistrationBase : RegistrationBase
    {
        private readonly string elementName;

        protected ManualRegistrationBase(IStructuralSearchPattern pattern, string elementName)
            : base(pattern)
        {
            this.elementName = elementName;
        }

        public override IComponentRegistrationCreator CreateComponentRegistrationCreator()
        {
            return new ComponentRegistrationCreator(elementName);
        }

        private class ComponentRegistrationCreator : IComponentRegistrationCreator
        {
            private readonly string elementName;

            public ComponentRegistrationCreator(string elementName)
            {
                this.elementName = elementName;
            }

            public IEnumerable<IComponentRegistration> CreateRegistrations(params IStructuralMatchResult[] matchResults)
            {
                foreach (IStructuralMatchResult match in matchResults)
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
}
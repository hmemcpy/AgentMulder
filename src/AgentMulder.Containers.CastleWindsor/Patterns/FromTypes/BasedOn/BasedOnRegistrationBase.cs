using System.Collections.Generic;
using System.ComponentModel.Composition;
using AgentMulder.ReSharper.Domain.Registrations;
using AgentMulder.ReSharper.Domain.Search;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.BasedOn
{
    [PartNotDiscoverable]
    public abstract class BasedOnRegistrationBase : RegistrationBase
    {
        private readonly string elementName;

        protected BasedOnRegistrationBase(IStructuralSearchPattern pattern, string elementName)
            : base(pattern)
        {
            this.elementName = elementName;
        }

        public override IComponentRegistrationCreator CreateComponentRegistrationCreator()
        {
            return new BasedOnRegistrationCreator(elementName);
        }

        private sealed class BasedOnRegistrationCreator : IComponentRegistrationCreator
        {
            private readonly string elementName;

            public BasedOnRegistrationCreator(string elementName)
            {
                this.elementName = elementName;
            }

            public IEnumerable<IComponentRegistration> CreateRegistrations(params IStructuralMatchResult[] matchResults)
            {
                foreach (var match in matchResults)
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
}
using System.Collections.Generic;
using System.ComponentModel.Composition;
using AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.BasedOn;
using AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.BasedOn.WhereArgument;
using AgentMulder.ReSharper.Domain.Patterns;
using AgentMulder.ReSharper.Domain.Registrations;

namespace AgentMulder.Containers.CastleWindsor.Providers
{
    [Export]
    public class BasedOnRegistrationProvider
    {
        private readonly List<IBasedOnPattern> whereArgumentPatterns = new List<IBasedOnPattern> 
        {
            new ComponentIsInNamespace(),
            new ComponentHasAttributeMethodGroup(),
            new ComponentIsInSameNamespaceAsGeneric(),
            new ComponentIsInSameNamespaceAsNonGeneric()
        };

        public IEnumerable<IBasedOnPattern> GetRegistrationPatterns(IBasedOnRegistrationCreator registrationCreator)
        {
            return new IBasedOnPattern[]
            {
                new BasedOnGeneric(registrationCreator),
                new BasedOnNonGeneric(registrationCreator),
                new InNamespace(),
                new InSameNamespaceAsGeneric(),
                new InSameNamespaceAsNonGeneric(),
                new Pick(registrationCreator),
                new Where(whereArgumentPatterns),
            };
        }
    }
}
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.BasedOn;
using AgentMulder.ReSharper.Domain.Patterns;
using AgentMulder.ReSharper.Domain.Registrations;

namespace AgentMulder.Containers.CastleWindsor.Providers
{
    [Export]
    public class BasedOnRegistrationProvider : IRegistrationPatternsProvider
    {
        private readonly WithServicesRegistrationProvider withServicesProvider;

        [ImportingConstructor]
        public BasedOnRegistrationProvider(WithServicesRegistrationProvider withServicesProvider)
        {
            this.withServicesProvider = withServicesProvider;
        }

        public IEnumerable<BasedOnRegistrationBasePattern> GetRegistrationPatterns()
        {
            var withServices = withServicesProvider.GetRegistrationPatterns().ToArray();

            return new BasedOnRegistrationBasePattern[]
            {
                new BasedOnGeneric(withServices),
                new BasedOnNonGeneric(withServices),
                new InNamespace(withServices),
                new InSameNamespaceAsGeneric(withServices),
                new InSameNamespaceAsNonGeneric(withServices),
                new Pick(withServices), 
            };
        }


        IEnumerable<RegistrationBasePattern> IRegistrationPatternsProvider.GetRegistrationPatterns()
        {
            return GetRegistrationPatterns();
        }
    }
}
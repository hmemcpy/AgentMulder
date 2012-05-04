using System.Collections.Generic;
using System.ComponentModel.Composition;
using AgentMulder.Containers.CastleWindsor.Patterns.Component.ImplementedBy;
using AgentMulder.ReSharper.Domain.Registrations;
using AgentMulder.ReSharper.Domain.Search;

namespace AgentMulder.Containers.CastleWindsor.Providers
{
    [Export]
    public class ImplementedByRegistrationProvider : IRegistrationPatternsProvider
    {
        public IEnumerable<ImplementedByBasePattern> GetRegistrationPatterns()
        {
            return new ImplementedByBasePattern[]
            {
                new ImplementedByGeneric(), 
                new ImplementedByNonGeneric()
            };
        }

        IEnumerable<RegistrationBasePattern> IRegistrationPatternsProvider.GetRegistrationPatterns()
        {
            return GetRegistrationPatterns();
        }
    }
}
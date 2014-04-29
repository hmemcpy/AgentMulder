using System.Collections.Generic;
using System.ComponentModel.Composition;
using AgentMulder.Containers.CastleWindsor.Patterns.Component.ImplementedBy;
using AgentMulder.ReSharper.Domain.Patterns;
using AgentMulder.ReSharper.Domain.Registrations;

namespace AgentMulder.Containers.CastleWindsor.Providers
{
    [Export]
    public class ImplementedByRegistrationProvider : IRegistrationPatternsProvider
    {
        public IEnumerable<ComponentImplementationPatternBase> GetRegistrationPatterns()
        {
            return new ComponentImplementationPatternBase[]
            {
                new ImplementedByGeneric(), 
                new ImplementedByNonGeneric()
            };
        }

        IEnumerable<IRegistrationPattern> IRegistrationPatternsProvider.GetRegistrationPatterns()
        {
            return GetRegistrationPatterns();
        }
    }
}
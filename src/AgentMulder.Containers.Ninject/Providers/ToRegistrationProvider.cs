using System.Collections.Generic;
using System.ComponentModel.Composition;
using AgentMulder.Containers.Ninject.Patterns.Module.To;
using AgentMulder.ReSharper.Domain.Registrations;
using AgentMulder.ReSharper.Domain.Search;

namespace AgentMulder.Containers.Ninject.Providers
{
    [Export]
    public class ToRegistrationProvider : IRegistrationPatternsProvider
    {
        public IEnumerable<ComponentImplementationBasePattern> GetRegistrationPatterns()
        {
            return new ComponentImplementationBasePattern[]
            {
                new ToGeneric(),
            };
        }

        IEnumerable<RegistrationBasePattern> IRegistrationPatternsProvider.GetRegistrationPatterns()
        {
            return GetRegistrationPatterns();
        }
    }
}
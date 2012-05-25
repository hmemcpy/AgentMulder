using System.Collections.Generic;
using System.ComponentModel.Composition;
using AgentMulder.Containers.Ninject.Patterns.Bind.To;
using AgentMulder.ReSharper.Domain.Patterns;
using AgentMulder.ReSharper.Domain.Registrations;

namespace AgentMulder.Containers.Ninject.Providers
{
    [Export]
    public class ToRegistrationProvider : IRegistrationPatternsProvider
    {
        public IEnumerable<ComponentImplementationPatternBase> GetRegistrationPatterns()
        {
            return new ComponentImplementationPatternBase[]
            {
                new ToGeneric(),
                new ToNonGeneric(),
                new ToSelf(),
            };
        }

        IEnumerable<IRegistrationPattern> IRegistrationPatternsProvider.GetRegistrationPatterns()
        {
            return GetRegistrationPatterns();
        }
    }
}
using System.Collections.Generic;
using System.ComponentModel.Composition;
using AgentMulder.ReSharper.Domain.Patterns;
using AgentMulder.ReSharper.Domain.Registrations;

namespace AgentMulder.Containers.StructureMap.Providers
{
    [Export]
    public class UsePatternsProvider : IRegistrationPatternsProvider
    {
        public IEnumerable<ComponentImplementationPatternBase> GetRegistrationPatterns()
        {
            return new ComponentImplementationPatternBase[]
            {
                // todo you know the drill :)
                // new UseGeneric()
            };
        }

        IEnumerable<IRegistrationPattern> IRegistrationPatternsProvider.GetRegistrationPatterns()
        {
            return GetRegistrationPatterns();
        }
    }
}
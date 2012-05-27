using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using AgentMulder.ReSharper.Domain.Patterns;
using AgentMulder.ReSharper.Domain.Registrations;

namespace AgentMulder.Containers.StructureMap.Providers
{
    [Export]
    [Export(typeof(IRegistrationPatternsProvider))]
    public class ForPatternsProvider : IRegistrationPatternsProvider
    {
        private readonly UsePatternsProvider usePatternsProvider;

        [ImportingConstructor]
        public ForPatternsProvider(UsePatternsProvider usePatternsProvider)
        {
            this.usePatternsProvider = usePatternsProvider;
        }

        public IEnumerable<IRegistrationPattern> GetRegistrationPatterns()
        {
            var usePatterns = usePatternsProvider.GetRegistrationPatterns().ToArray();

            return new IRegistrationPattern[]
            {
                // todo add patterns here :)
                // e.g:
                // new ForGeneric(usePatterns);
            };
        }
    }
}
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using AgentMulder.Containers.CastleWindsor.Patterns.FromTypes;
using AgentMulder.ReSharper.Domain.Registrations;
using AgentMulder.ReSharper.Domain.Search;

namespace AgentMulder.Containers.CastleWindsor.Providers
{
    [Export]
    [Export(typeof(IRegistrationPatternsProvider))]
    public class TypesRegistrationProvider : IRegistrationPatternsProvider
    {
        private readonly BasedOnRegistrationProvider basedOnProvider;

        [ImportingConstructor]
        public TypesRegistrationProvider(BasedOnRegistrationProvider basedOnProvider)
        {
            this.basedOnProvider = basedOnProvider;
        }

        public IEnumerable<RegistrationBasePattern> GetRegistrationPatterns()
        {
            var basedOnPatterns = basedOnProvider.GetRegistrationPatterns().ToArray();

            return new FromTypesBasePattern[]
            {
                new TypesFrom(basedOnPatterns),
                new TypesFromAssembly(basedOnPatterns),
                new TypesFromThisAssembly(basedOnPatterns)
            };
        }
    }
}
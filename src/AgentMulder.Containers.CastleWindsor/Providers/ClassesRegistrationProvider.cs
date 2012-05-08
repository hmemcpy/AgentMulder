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
    public class ClassesRegistrationProvider : IRegistrationPatternsProvider
    {
        private readonly BasedOnRegistrationProvider basedOnProvider;

        [ImportingConstructor]
        public ClassesRegistrationProvider(BasedOnRegistrationProvider basedOnProvider)
        {
            this.basedOnProvider = basedOnProvider;
        }

        public IEnumerable<RegistrationBasePattern> GetRegistrationPatterns()
        {
            var basedOnPatterns = basedOnProvider.GetRegistrationPatterns().ToArray();

            return new FromTypesBasePattern[]
            {
                new ClassesFrom(basedOnPatterns),
                new ClassesFromAssembly(basedOnPatterns),
                new ClassesFromThisAssembly(basedOnPatterns),
                new ClassesFromAssemblyNamed(basedOnPatterns), 
            };
        }
    }
}
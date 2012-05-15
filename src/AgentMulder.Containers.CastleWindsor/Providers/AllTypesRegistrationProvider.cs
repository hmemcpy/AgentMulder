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
    public class AllTypesRegistrationProvider : IRegistrationPatternsProvider
    {
        private const string AllTypesFullTypeName = "Castle.MicroKernel.Registration.AllTypes";
        private readonly BasedOnRegistrationProvider basedOnProvider;

        [ImportingConstructor]
        public AllTypesRegistrationProvider(BasedOnRegistrationProvider basedOnProvider)
        {
            this.basedOnProvider = basedOnProvider;
        }

        public IEnumerable<RegistrationBasePattern> GetRegistrationPatterns()
        {
            var basedOnPatterns = basedOnProvider.GetRegistrationPatterns().ToArray();

            return new FromTypesBasePattern[]
            {
                new AllTypesFrom(basedOnPatterns),
                new FromAssemblyPattern(AllTypesFullTypeName, basedOnPatterns), 
                //new AllTypesFromAssembly(basedOnPatterns),
                new AllTypesFromThisAssembly(basedOnPatterns),
                new AllTypesFromAssemblyNamed(basedOnPatterns),
                new AllTypesFromAssemblyContainingGeneric(basedOnPatterns),
                new AllTypesFromAssemblyContainingNonGeneric(basedOnPatterns)
            };
        }
    }
}
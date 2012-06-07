using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using AgentMulder.Containers.CastleWindsor.Patterns.FromTypes;
using AgentMulder.Containers.CastleWindsor.Registrations;
using AgentMulder.ReSharper.Domain.Patterns;
using AgentMulder.ReSharper.Domain.Registrations;

namespace AgentMulder.Containers.CastleWindsor.Providers
{
    [Export]
    [Export(typeof(IRegistrationPatternsProvider))]
    public class TypesRegistrationProvider : IRegistrationPatternsProvider
    {
        private const string TypesFullTypeName = "Castle.MicroKernel.Registration.Types";

        private readonly BasedOnRegistrationProvider basedOnProvider;

        [ImportingConstructor]
        public TypesRegistrationProvider(BasedOnRegistrationProvider basedOnProvider)
        {
            this.basedOnProvider = basedOnProvider;
        }

        public IEnumerable<IRegistrationPattern> GetRegistrationPatterns()
        {
            var basedOnPatterns = basedOnProvider.GetRegistrationPatterns(new TypesRegistrationCreator()).ToArray();

            return new FromDescriptorPatternBase[]
            {
                new From(TypesFullTypeName, basedOnPatterns),
                new FromAssembly(TypesFullTypeName, basedOnPatterns),
                new FromThisAssembly(TypesFullTypeName, basedOnPatterns),
                new FromAssemblyNamed(TypesFullTypeName, basedOnPatterns), 
                new FromAssemblyContainingGeneric(TypesFullTypeName, basedOnPatterns),
                new FromAssemblyContainingNonGeneric(TypesFullTypeName, basedOnPatterns)
            };
        }
    }
}
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
        private const string TypesFullTypeName = "global::Castle.MicroKernel.Registration.Types";

        private readonly BasedOnRegistrationProvider basedOnProvider;

        [ImportingConstructor]
        public TypesRegistrationProvider(BasedOnRegistrationProvider basedOnProvider)
        {
            this.basedOnProvider = basedOnProvider;
        }

        public IEnumerable<IRegistrationPattern> GetRegistrationPatterns()
        {
            var basedOnPatterns = basedOnProvider.GetRegistrationPatterns(new TypesRegistrationCreator()).ToArray();

            return new IRegistrationPattern[]
            {
                new CompositePattern(new From(TypesFullTypeName), basedOnPatterns),
                new CompositePattern(new FromAssembly(TypesFullTypeName), basedOnPatterns), 
                new CompositePattern(new FromThisAssembly(TypesFullTypeName), basedOnPatterns),
                new CompositePattern(new FromAssemblyNamed(TypesFullTypeName), basedOnPatterns), 
                new CompositePattern(new FromAssemblyContainingGeneric(TypesFullTypeName), basedOnPatterns),
                new CompositePattern(new FromAssemblyContainingNonGeneric(TypesFullTypeName), basedOnPatterns)
            };
        }
    }
}
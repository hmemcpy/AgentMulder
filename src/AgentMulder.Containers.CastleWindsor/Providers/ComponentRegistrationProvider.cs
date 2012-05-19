using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using AgentMulder.Containers.CastleWindsor.Patterns.Component.ComponentFor;
using AgentMulder.ReSharper.Domain.Registrations;
using AgentMulder.ReSharper.Domain.Search;

namespace AgentMulder.Containers.CastleWindsor.Providers
{
    [Export]
    [Export(typeof(IRegistrationPatternsProvider))]
    public class ComponentRegistrationProvider : IRegistrationPatternsProvider
    {
        private readonly ImplementedByRegistrationProvider implementedByProvider;

        [ImportingConstructor]
        public ComponentRegistrationProvider(ImplementedByRegistrationProvider implementedByProvider)
        {
            this.implementedByProvider = implementedByProvider;
        }

        public IEnumerable<RegistrationBasePattern> GetRegistrationPatterns()
        {
            var implementedByPatterns = implementedByProvider.GetRegistrationPatterns().ToArray();

            return new ComponentForBasePattern[]
            {
                new ComponentForNonGeneric(implementedByPatterns),
                new ComponentForGeneric(implementedByPatterns), // Component.For<>
                new ComponentForGeneric(1, implementedByPatterns), // Component.For<,>
                new ComponentForGeneric(2, implementedByPatterns), // Component.For<,,>
                new ComponentForGeneric(3, implementedByPatterns), // Component.For<,,,>
                new ComponentForGeneric(4, implementedByPatterns), // Component.For<,,,,>
            };
        }
    }
}
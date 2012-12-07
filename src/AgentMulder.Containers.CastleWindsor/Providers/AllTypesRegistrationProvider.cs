using System;
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
    public class AllTypesRegistrationProvider : IRegistrationPatternsProvider
    {
        private const string AllTypesFullTypeName = "global::Castle.MicroKernel.Registration.AllTypes";

        private readonly BasedOnRegistrationProvider basedOnProvider;

        [ImportingConstructor]
        public AllTypesRegistrationProvider(BasedOnRegistrationProvider basedOnProvider)
        {
            this.basedOnProvider = basedOnProvider;
        }

        public IEnumerable<IRegistrationPattern> GetRegistrationPatterns()
        {
            var basedOnPatterns = basedOnProvider.GetRegistrationPatterns(new ClassesRegistrationCreator()).ToArray();

            return new IRegistrationPattern[]
            {
                new CompositePattern(new From(AllTypesFullTypeName), basedOnPatterns),
                new CompositePattern(new FromAssembly(AllTypesFullTypeName), basedOnPatterns), 
                new CompositePattern(new FromThisAssembly(AllTypesFullTypeName), basedOnPatterns),
                new CompositePattern(new FromAssemblyNamed(AllTypesFullTypeName), basedOnPatterns), 
                new CompositePattern(new FromAssemblyContainingGeneric(AllTypesFullTypeName), basedOnPatterns),
                new CompositePattern(new FromAssemblyContainingNonGeneric(AllTypesFullTypeName), basedOnPatterns)
            };
        }
    }
}
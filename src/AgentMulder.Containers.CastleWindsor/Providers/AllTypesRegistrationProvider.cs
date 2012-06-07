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
        private const string AllTypesFullTypeName = "Castle.MicroKernel.Registration.AllTypes";

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
                new From(AllTypesFullTypeName, basedOnPatterns),
                new FromAssembly(AllTypesFullTypeName, basedOnPatterns), 
                new FromThisAssembly(AllTypesFullTypeName, basedOnPatterns),
                new FromAssemblyNamed(AllTypesFullTypeName, basedOnPatterns), 
                new FromAssemblyContainingGeneric(AllTypesFullTypeName, basedOnPatterns),
                new FromAssemblyContainingNonGeneric(AllTypesFullTypeName, basedOnPatterns)
            };
        }
    }
}
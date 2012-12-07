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
    public class ClassesRegistrationProvider : IRegistrationPatternsProvider
    {
        private const string ClassesFullTypeName = "global::Castle.MicroKernel.Registration.Classes";

        private readonly BasedOnRegistrationProvider basedOnProvider;

        [ImportingConstructor]
        public ClassesRegistrationProvider(BasedOnRegistrationProvider basedOnProvider)
        {
            this.basedOnProvider = basedOnProvider;
        }

        public IEnumerable<IRegistrationPattern> GetRegistrationPatterns()
        {
            var basedOnPatterns = basedOnProvider.GetRegistrationPatterns(new ClassesRegistrationCreator()).ToArray();

            return new IRegistrationPattern[]
            {
                new CompositePattern(new From(ClassesFullTypeName), basedOnPatterns),
                new CompositePattern(new FromAssembly(ClassesFullTypeName), basedOnPatterns), 
                new CompositePattern(new FromThisAssembly(ClassesFullTypeName), basedOnPatterns),
                new CompositePattern(new FromAssemblyNamed(ClassesFullTypeName), basedOnPatterns), 
                new CompositePattern(new FromAssemblyContainingGeneric(ClassesFullTypeName), basedOnPatterns),
                new CompositePattern(new FromAssemblyContainingNonGeneric(ClassesFullTypeName), basedOnPatterns)
            };
        }
    }
}
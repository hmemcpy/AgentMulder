using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using AgentMulder.Containers.CastleWindsor.Patterns.FromTypes;
using AgentMulder.ReSharper.Domain.Registrations;
using AgentMulder.ReSharper.Domain.Search;
using JetBrains.ReSharper.Psi;

namespace AgentMulder.Containers.CastleWindsor.Providers
{
    [Export]
    [Export(typeof(IRegistrationPatternsProvider))]
    public class ClassesRegistrationProvider : IRegistrationPatternsProvider
    {
        public static readonly Predicate<ITypeElement> Filter = typeElement =>
        {
            var @class = typeElement as IClass;
            if (@class != null)
            {
                return !@class.IsAbstract;
            }

            return false;
        };

        private const string ClassesFullTypeName = "Castle.MicroKernel.Registration.Classes";

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
                new From(ClassesFullTypeName, Filter, basedOnPatterns),
                new FromAssembly(ClassesFullTypeName, Filter, basedOnPatterns), 
                new FromThisAssembly(ClassesFullTypeName, Filter, basedOnPatterns),
                new FromAssemblyNamed(ClassesFullTypeName, Filter, basedOnPatterns), 
                new FromAssemblyContainingGeneric(ClassesFullTypeName, Filter, basedOnPatterns),
                new FromAssemblyContainingNonGeneric(ClassesFullTypeName, Filter, basedOnPatterns)
            };
        }
    }
}
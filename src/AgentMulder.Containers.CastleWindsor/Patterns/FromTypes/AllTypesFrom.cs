using System;
using System.Collections.Generic;
using System.Linq;
using AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.BasedOn;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.Containers.CastleWindsor.Patterns.FromTypes
{
    internal sealed class AllTypesFrom : FromTypesBasePattern
    {
        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$alltypes$.From($services$)",
                                              new ExpressionPlaceholder("alltypes", "Castle.MicroKernel.Registration.AllTypes"),
                                              new ArgumentPlaceholder("services", -1, -1));

        // matches any number of arguments

        public AllTypesFrom(params BasedOnRegistrationBasePattern[] basedOnPatterns)
            : base(pattern, basedOnPatterns)
        {
        }

        protected override IComponentRegistration CreateRegistration(IStructuralMatchResult match, BasedOnRegistration basedOnRegistration, IEnumerable<ITypeElement> typeElements)
        {
            // get all non-abstract classes
            typeElements = typeElements.OfType<IClass>().Where(c => !c.IsAbstract);

            return base.CreateRegistration(match, basedOnRegistration, typeElements);
        }
    }
}
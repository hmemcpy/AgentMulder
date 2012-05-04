using System.Collections.Generic;
using AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.BasedOn;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.Containers.CastleWindsor.Patterns.FromTypes
{
    internal sealed class TypesFrom : FromTypesBasePattern
    {
        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$types$.From($services$)",
                                              new ExpressionPlaceholder("types", "Castle.MicroKernel.Registration.Types"),
                                              new ArgumentPlaceholder("services", -1, -1)); // matches any number of arguments

        public TypesFrom(params BasedOnRegistrationBasePattern[] basedOnPatterns)
            : base(pattern, basedOnPatterns)
        {
        }

        protected override IComponentRegistration CreateRegistration(IStructuralMatchResult match, BasedOnRegistration basedOnRegistration, IEnumerable<ITypeElement> typeElements)
        {
            return new TypesBasedOnRegistration(typeElements, basedOnRegistration);
        }
    }
}
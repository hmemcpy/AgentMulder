using System;
using AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.BasedOn;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.Containers.CastleWindsor.Patterns.FromTypes
{
    internal sealed class From : FromTypesPatternBase
    {
        public From(string qualiferType, params IBasedOnPattern[] basedOnPatterns)
            : this(qualiferType, element => true, basedOnPatterns)
        {
        }

        public From(string qualiferType, Predicate<ITypeElement> filter, params IBasedOnPattern[] basedOnPatterns)
            : base(CreatePattern(qualiferType), filter, basedOnPatterns)
        {
        }

        private static IStructuralSearchPattern CreatePattern(string qualiferType)
        {
            return new CSharpStructuralSearchPattern("$qualifier$.From($services$)",
                new ExpressionPlaceholder("qualifier", qualiferType),
                new ArgumentPlaceholder("services", -1, -1)); // matches any number of arguments
        }
    }
}
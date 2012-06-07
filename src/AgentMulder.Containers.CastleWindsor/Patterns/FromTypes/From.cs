using System;
using AgentMulder.ReSharper.Domain.Patterns;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.Containers.CastleWindsor.Patterns.FromTypes
{
    internal sealed class From : FromTypesPatternBase
    {
        public From(string qualiferType, params IBasedOnPattern[] basedOnPatterns)
            : base(CreatePattern(qualiferType), basedOnPatterns)
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
using System;
using System.ComponentModel.Composition;
using AgentMulder.ReSharper.Domain.Patterns;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.Containers.StructureMap.Patterns.For
{
    internal sealed class ForGeneric : ForBasePattern
    {
        private static readonly IStructuralSearchPattern pattern = 
            new CSharpStructuralSearchPattern("$registry$.For<$service$>()",
                new ExpressionPlaceholder("registry", "global::StructureMap.Configuration.DSL.IRegistry", false),
                new TypePlaceholder("service"));

        [ImportingConstructor]
        public ForGeneric([ImportMany] params ComponentImplementationPatternBase[] usePatterns)
            : base(pattern, "service", usePatterns)
        {
        }
    }
}
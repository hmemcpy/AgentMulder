using System.ComponentModel.Composition;
using AgentMulder.ReSharper.Domain.Patterns;
using JetBrains.ReSharper.Feature.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Feature.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Feature.Services.StructuralSearch;

namespace AgentMulder.Containers.StructureMap.Patterns.For
{
    internal sealed class RegistryForGeneric : ForBasePattern
    {
        private static readonly IStructuralSearchPattern pattern = 
            new CSharpStructuralSearchPattern("For<$service$>()",
                new TypePlaceholder("service"));

        [ImportingConstructor]
        public RegistryForGeneric([ImportMany] params ComponentImplementationPatternBase[] usePatterns)
            : base(pattern, "service", usePatterns)
        {
        }
    }
}
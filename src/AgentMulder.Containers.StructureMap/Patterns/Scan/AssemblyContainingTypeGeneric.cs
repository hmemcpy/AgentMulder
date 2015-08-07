using System.ComponentModel.Composition;
using AgentMulder.ReSharper.Domain.Elements.Modules;
using AgentMulder.ReSharper.Domain.Patterns;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Feature.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Feature.Services.StructuralSearch;

namespace AgentMulder.Containers.StructureMap.Patterns.Scan
{
    [Export("FromAssembly", typeof(ModuleBasedPatternBase))]
    public class AssemblyContainingTypeGeneric : ModuleBasedPatternBase
    {
        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$scanner$.AssemblyContainingType<$type$>()",
                new ExpressionPlaceholder("scanner", "global::StructureMap.Graph.IAssemblyScanner", false),
                new TypePlaceholder("type"));
        
        public AssemblyContainingTypeGeneric()
            : base(pattern)
        {
        }

        protected override IModule GetTargetModule(IStructuralMatchResult match)
        {
            return ModuleExtractor.GetTargetModule(match.GetMatchedType("type"));
        }
    }
}
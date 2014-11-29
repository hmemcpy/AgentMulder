using System.ComponentModel.Composition;
using AgentMulder.ReSharper.Domain.Elements.Modules;
using AgentMulder.ReSharper.Domain.Patterns;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.CSharp.Tree;
#if SDK90
using JetBrains.ReSharper.Feature.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Feature.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Feature.Services.StructuralSearch;
#else
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
#endif

namespace AgentMulder.Containers.StructureMap.Patterns.Scan
{
    [Export("FromAssembly", typeof(ModuleBasedPatternBase))]
    internal sealed class Assembly : ModuleBasedPatternBase
    {
        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$scanner$.Assembly($assembly$)",
                new ExpressionPlaceholder("scanner", "global::StructureMap.Graph.IAssemblyScanner", false),
                new ArgumentPlaceholder("assembly", 1, 1));

        public Assembly()
            : base(pattern)
        {
        }

        protected override IModule GetTargetModule(IStructuralMatchResult match)
        {
            var argument = (ICSharpArgument)match.GetMatchedElement("assembly");

            return ModuleExtractor.GetTargetModule(argument.Value);
        }
    }
}
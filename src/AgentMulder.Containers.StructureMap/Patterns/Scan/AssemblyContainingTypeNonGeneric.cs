using System.ComponentModel.Composition;
using AgentMulder.ReSharper.Domain.Elements.Modules;
using AgentMulder.ReSharper.Domain.Patterns;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.Containers.StructureMap.Patterns.Scan
{
    [Export("FromAssembly", typeof(ModuleBasedPatternBase))]
    public class AssemblyContainingTypeNonGeneric : ModuleBasedPatternBase
    {
        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$scanner$.AssemblyContainingType($argument$)",
                new ExpressionPlaceholder("scanner", "global::StructureMap.Graph.IAssemblyScanner"),
                new ArgumentPlaceholder("argument", 1, 1));
        
        public AssemblyContainingTypeNonGeneric()
            : base(pattern)
        {
        }

        protected override IModule GetTargetModule(IStructuralMatchResult match)
        {
            var argument = (ICSharpArgument)match.GetMatchedElement("argument");

            return ModuleExtractor.GetTargetModule(argument.Value);
        }
    }
}
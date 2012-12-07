using System.ComponentModel.Composition;
using AgentMulder.ReSharper.Domain.Patterns;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.Containers.StructureMap.Patterns.Scan
{
    [Export("FromAssembly", typeof(ModuleBasedPatternBase))]
    public class TheCallingAssembly : ModuleBasedPatternBase
    {
        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$scanner$.TheCallingAssembly()",
                new ExpressionPlaceholder("scanner", "global::StructureMap.Graph.IAssemblyScanner", false));
        
        public TheCallingAssembly()
            : base(pattern)
        {
        }

        protected override IModule GetTargetModule(IStructuralMatchResult match)
        {
            return match.MatchedElement.GetPsiModule().ContainingProjectModule;
        }
    }
}
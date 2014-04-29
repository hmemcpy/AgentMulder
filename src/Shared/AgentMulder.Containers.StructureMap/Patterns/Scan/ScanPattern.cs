using System.Collections.Generic;
using System.ComponentModel.Composition;
using AgentMulder.ReSharper.Domain.Patterns;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.Containers.StructureMap.Patterns.Scan
{
    internal sealed class ScanPattern : ScanPatternBase
    {
        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$registry$.Scan($id$ => { $statements$ })",
            // ReSharper disable RedundantArgumentDefaultValue
                new ExpressionPlaceholder("registry", "global::StructureMap.Configuration.DSL.IRegistry", false),
            // ReSharper restore RedundantArgumentDefaultValue
                new ArgumentPlaceholder("id"),
                new StatementPlaceholder("statements", -1, -1));

        [ImportingConstructor]
        public ScanPattern([ImportMany("FromAssembly")] IEnumerable<ModuleBasedPatternBase> fromAssemblyPatterns,
                           [ImportMany] IEnumerable<IBasedOnPattern> basedOnPatterns)
            : base(pattern, fromAssemblyPatterns, basedOnPatterns)
        {
        }
    }
}
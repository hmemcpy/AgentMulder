using System.Collections.Generic;
using System.ComponentModel.Composition;
using AgentMulder.ReSharper.Domain.Patterns;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.Containers.StructureMap.Patterns.Scan
{
    internal sealed class RegistryScanPattern : ScanPatternBase
    {
        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("Scan($id$ => { $statements$ })",
                new ArgumentPlaceholder("id"),
                new StatementPlaceholder("statements", -1, -1));

        [ImportingConstructor]
        public RegistryScanPattern([ImportMany("FromAssembly")] IEnumerable<FromAssemblyPatternBase> fromAssemblyPatterns, [ImportMany] IEnumerable<IBasedOnPattern> basedOnPatterns)
            : base(pattern, fromAssemblyPatterns, basedOnPatterns)
        {
        }
    }
}
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using AgentMulder.ReSharper.Domain.Patterns;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.StructureMap.Patterns.Scan
{
    [Export("ComponentRegistration", typeof(IRegistrationPattern))]
    internal sealed class ScanStatements : FromDescriptorPatternBase
    {
        private static readonly IStructuralSearchPattern pattern = 
            new CSharpStructuralSearchPattern("$registry$.Scan($arguments$)",
                new ExpressionPlaceholder("registry", "global::StructureMap.Configuration.DSL.IRegistry"),
                new ArgumentPlaceholder("arguments", -1, -1));

        [ImportingConstructor]
        public ScanStatements([ImportMany] params IBasedOnPattern[] basedOnPatterns)
            : base(pattern, basedOnPatterns)
        {
        }

        public override IEnumerable<IComponentRegistration> GetComponentRegistrations(ITreeNode registrationRootElement)
        {
            return BasedOnPatterns.SelectMany(basedOnPattern => basedOnPattern.GetBasedOnRegistrations(registrationRootElement));
        }
    }
}
using System.Collections.Generic;
using System.ComponentModel.Composition;
using AgentMulder.Containers.StructureMap.Registrations;
using AgentMulder.ReSharper.Domain.Patterns;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Feature.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Feature.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Feature.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.StructureMap.Patterns.Scan
{
    [Export(typeof(IBasedOnPattern))]
    internal sealed class SingleImplementationsOfInterface : BasedOnPatternBase
    {
        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$scanner$.SingleImplementationsOfInterface()",
                new ExpressionPlaceholder("scanner", "global::StructureMap.Graph.IAssemblyScanner", false));

        public SingleImplementationsOfInterface()
            : base(pattern)
        {
        }

        public override IEnumerable<IComponentRegistration> GetComponentRegistrations(ITreeNode registrationRootElement)
        {
            return GetBasedOnRegistrations(registrationRootElement);
        }

        public override IEnumerable<FilteredRegistrationBase> GetBasedOnRegistrations(ITreeNode registrationRootElement)
        {
            var match = Match(registrationRootElement);

            if (match.Matched)
            {
                yield return new SingleImplementationsOfInterfaceConvention(registrationRootElement);
            }
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using AgentMulder.ReSharper.Domain.Patterns;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.StructureMap.Patterns
{
    public abstract class ScanStatementsPatternBase : FromDescriptorPatternBase
    {
        protected ScanStatementsPatternBase(IStructuralSearchPattern pattern, params IBasedOnPattern[] basedOnPatterns)
            : base(pattern, basedOnPatterns)
        {
        }

        public override IEnumerable<IComponentRegistration> GetComponentRegistrations(ITreeNode registrationRootElement)
        {
            return BasedOnPatterns.SelectMany(basedOnPattern => basedOnPattern.GetBasedOnRegistrations(registrationRootElement));
        }
    }
}
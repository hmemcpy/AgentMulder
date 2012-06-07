using System;
using System.Collections.Generic;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.ReSharper.Domain.Patterns
{
    public abstract class FromDescriptorPatternBase : RegistrationPatternBase
    {
        private readonly IEnumerable<IBasedOnPattern> basedOnPatterns;

        protected IEnumerable<IBasedOnPattern> BasedOnPatterns
        {
            get { return basedOnPatterns; }
        }

        protected FromDescriptorPatternBase(IStructuralSearchPattern pattern, IEnumerable<IBasedOnPattern> basedOnPatterns)
            : base(pattern)
        {
            this.basedOnPatterns = basedOnPatterns;
        }
    }
}
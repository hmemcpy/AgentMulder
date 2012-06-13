using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.ReSharper.Domain.Patterns
{
    [InheritedExport("ComponentRegistration", typeof(IRegistrationPattern))]
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
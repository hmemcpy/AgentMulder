using System;
using System.Collections.Generic;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.ReSharper.Domain.Patterns
{
    public abstract class FromDescriptorPatternBase : RegistrationPatternBase
    {
        private readonly Predicate<ITypeElement> filter;
        private readonly IEnumerable<IBasedOnPattern> basedOnPatterns;

        protected IEnumerable<IBasedOnPattern> BasedOnPatterns
        {
            get { return basedOnPatterns; }
        }

        protected virtual Predicate<ITypeElement> Filter
        {
            get { return filter; }
        }

        protected FromDescriptorPatternBase(IStructuralSearchPattern pattern, Predicate<ITypeElement> filter, IEnumerable<IBasedOnPattern> basedOnPatterns)
            : base(pattern)
        {
            this.filter = filter;
            this.basedOnPatterns = basedOnPatterns;
        }
    }
}
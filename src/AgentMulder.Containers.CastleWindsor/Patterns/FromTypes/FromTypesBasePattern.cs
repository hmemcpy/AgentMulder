using System;
using System.Collections.Generic;
using AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.BasedOn;
using AgentMulder.ReSharper.Domain.Search;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.Containers.CastleWindsor.Patterns.FromTypes
{
    public abstract class FromTypesBasePattern : RegistrationBasePattern
    {
        protected readonly IEnumerable<BasedOnRegistrationBasePattern> basedOnPatterns;

        protected FromTypesBasePattern(IStructuralSearchPattern pattern, IEnumerable<BasedOnRegistrationBasePattern> basedOnPatterns)
            : base(pattern)
        {
            this.basedOnPatterns = basedOnPatterns;
        }
    }
}
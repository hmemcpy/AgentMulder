using System;
using System.Collections.Generic;
using AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.BasedOn;
using AgentMulder.ReSharper.Domain.Search;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.Containers.CastleWindsor.Patterns.FromTypes
{
    public abstract class FromTypesBase : RegistrationBase
    {
        protected readonly IEnumerable<BasedOnRegistrationBase> basedOnPatterns;

        protected FromTypesBase(IStructuralSearchPattern pattern, IEnumerable<BasedOnRegistrationBase> basedOnPatterns)
            : base(pattern)
        {
            this.basedOnPatterns = basedOnPatterns;
        }
    }
}
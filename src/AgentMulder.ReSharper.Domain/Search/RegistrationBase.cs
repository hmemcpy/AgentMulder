using System;
using System.Collections.Generic;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.Util;

namespace AgentMulder.ReSharper.Domain.Search
{
    public abstract class RegistrationBase : IRegistrationPattern
    {
        private readonly IStructuralSearchPattern pattern;

        protected RegistrationBase(IStructuralSearchPattern pattern)
        {
            this.pattern = pattern;
            Assertion.Assert(pattern.Check() == null, "Invalid pattern");
        }

        public IStructuralSearchPattern Pattern
        {
            get { return pattern; }
        }

        public virtual IStructuralMatcher CreateMatcher()
        {
            return pattern.CreateMatcher();
        }

        public abstract IEnumerable<IComponentRegistration> GetComponentRegistrations(params IStructuralMatchResult[] matchResults);
    }
}
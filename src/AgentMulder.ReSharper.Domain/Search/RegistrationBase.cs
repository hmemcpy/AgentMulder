using System.ComponentModel.Composition;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.ReSharper.Domain.Search
{
    [Export(typeof(IRegistrationPattern))]
    public abstract class RegistrationBase : IRegistrationPattern
    {
        private readonly IStructuralSearchPattern pattern;

        protected RegistrationBase(IStructuralSearchPattern pattern)
        {
            this.pattern = pattern;
        }

        public virtual IStructuralMatcher CreateMatcher()
        {
            return pattern.CreateMatcher();
        }

        public abstract IComponentRegistrationCreator CreateComponentRegistrationCreator();
    }
}
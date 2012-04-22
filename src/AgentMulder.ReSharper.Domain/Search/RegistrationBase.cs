using System.ComponentModel.Composition;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.ReSharper.Domain.Search
{
    [Export(typeof(IRegistration))]
    public abstract class RegistrationBase : IRegistration
    {
        private readonly IStructuralSearchPattern pattern;

        protected RegistrationBase(IStructuralSearchPattern pattern)
        {
            this.pattern = pattern;
        }

        public IStructuralMatcher CreateMatcher()
        {
            return pattern.CreateMatcher();
        }

        public abstract IComponentRegistrationCreator CreateComponentRegistrationCreator();
    }
}
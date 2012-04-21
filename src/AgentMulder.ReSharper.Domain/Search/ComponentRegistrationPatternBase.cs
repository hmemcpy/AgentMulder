using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.ReSharper.Domain.Search
{
    public abstract class ComponentRegistrationPatternBase : IComponentRegistrationPattern
    {
        private readonly IStructuralSearchPattern pattern;

        protected ComponentRegistrationPatternBase(IStructuralSearchPattern pattern)
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
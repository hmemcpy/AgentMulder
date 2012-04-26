using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.Search;

namespace AgentMulder.ReSharper.Domain.Search
{
    public class SolutionSearcher : PatternSearcher
    {
        public SolutionSearcher(ISolution solution)
            : base(solution, SearchDomainFactory.Instance)
        {
        }

        protected override ISearchDomain GetSearchDomain()
        {
            return searchDomainFactory.CreateSearchDomain(solution, false);
        }
    }
}
using AgentMulder.TypeSystem.ProjectModel;
using AgentMulder.TypeSystem.ProjectModel.Impl;

namespace AgentMulder.TypeSystem.Adapters
{
    using JbISolution = JetBrains.ProjectModel.ISolution;

    public class SolutionAdapter
    {
        private readonly JbISolution jbSolution;

        public SolutionAdapter(JbISolution jbSolution)
        {
            this.jbSolution = jbSolution;
        }

        public ISolution CreateSolution()
        {
            var result = new Solution(jbSolution);
            
            return result;
        }
    }
}
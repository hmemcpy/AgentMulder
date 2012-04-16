using System.Collections.Generic;
using System.Linq;
using ICSharpCode.NRefactory.CSharp.TypeSystem;
using ICSharpCode.NRefactory.TypeSystem;
using ICSharpCode.NRefactory.TypeSystem.Implementation;

namespace AgentMulder.TypeSystem.ProjectModel.Impl
{
    using JbISolution = JetBrains.ProjectModel.ISolution;

    internal class Solution : ISolution
    {
        private readonly string name;
        private readonly ISolutionSnapshot solutionSnapshot = new DefaultSolutionSnapshot();
        private readonly List<ICSharpProject> projects = new List<ICSharpProject>();

        public Solution(JbISolution jbSolution)
        {
            name = jbSolution.Name;
            
            projects.AddRange(
                jbSolution.GetAllProjects().Select(project => new CSharpProject(this, project)));
        }

        public string Name
        {
            get { return name; }
        }

        public IEnumerable<ICSharpProject> Projects
        {
            get { return projects; }
        }

        public ISolutionSnapshot SolutionSnapshot
        {
            get { return solutionSnapshot; }
        }
    }
}
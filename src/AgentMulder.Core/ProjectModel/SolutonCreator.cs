using AgentMulder.Core.ProjectModel.CSharp;

namespace AgentMulder.Core.ProjectModel
{
    public class SolutonReader
    {
        public static ISolution ReadSolution(string fileName)
        {
            return new Solution(fileName);
        }
    }
}
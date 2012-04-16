using AgentMulder.Core.TypeSystem.Impl;

namespace AgentMulder.Core.TypeSystem
{
    public class SolutonReader
    {
        public static ISolution ReadSolution(string fileName)
        {
            return new Solution(fileName);
        }
    }
}
using System;
using System.Linq;
using ICSharpCode.NRefactory.ConsistencyCheck;
using NUnit.Framework;

namespace AgentMulder.Core.Tests
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void UnderTest_Scenario_ExpectedResult()
        {
            Solution s = new Solution(@"D:\dev\AgentMulder\src\AgentMulder.sln");
            CSharpProject project =
                s.Projects.Find(p => p.Compilation.Assemblies.Any(a => a.AssemblyName == "Castle.Windsor"));
            
            WindsorAnalyzer windsorAnalyzer = new WindsorAnalyzer();
           
            windsorAnalyzer.Analyze(project);
            
        }    
    }
}
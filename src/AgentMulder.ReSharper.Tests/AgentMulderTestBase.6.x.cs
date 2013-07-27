using System;
using System.Collections.Generic;

namespace AgentMulder.ReSharper.Tests
{
    public abstract partial class AgentMulderTestBase
    {
        private void RunFixture(IEnumerable<string> fileSet, Action action)
        {
            WithSingleProject(fileSet, (lifetime, project) => RunGuarded(action));
        }
    }
}
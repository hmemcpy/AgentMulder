using System.Collections.Generic;
using JetBrains.ReSharper.TestFramework;
using Ninject.Modules;

namespace AgentMulder.ReSharper.Tests.Ninject
{
    public class TestNinjectAttribute : TestReferencesAttribute
    {
        public override IEnumerable<string> GetReferences()
        {
            return new[]
            {
                typeof(NinjectModule).Assembly.Location
            };
        }
    }
}
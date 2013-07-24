using System.Collections.Generic;
using JetBrains.ReSharper.TestFramework;
using Ninject.Modules;

namespace AgentMulder.ReSharper.Tests.Ninject.Helpers
{
    public class TestNinjectAttribute : TestReferencesAttribute
    {
#if SDK70 || SDK80
        public override IEnumerable<string> GetReferences()
#else
        public override string[] GetReferences()
#endif
        {
            return new[]
            {
                typeof(NinjectModule).Assembly.Location
            };
        }
    }
}
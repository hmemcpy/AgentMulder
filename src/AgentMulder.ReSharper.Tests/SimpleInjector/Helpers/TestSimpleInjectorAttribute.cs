using System.Collections.Generic;
using JetBrains.ReSharper.TestFramework;
using SimpleInjector;

namespace AgentMulder.ReSharper.Tests.SimpleInjector.Helpers
{
    public class TestSimpleInjectorAttribute : TestReferencesAttribute
    {
#if SDK70
        public override IEnumerable<string> GetReferences()
#else
        public override string[] GetReferences()
#endif
        {
            return new[]
            {
                typeof(Container).Assembly.Location
            };
        }
    }
}
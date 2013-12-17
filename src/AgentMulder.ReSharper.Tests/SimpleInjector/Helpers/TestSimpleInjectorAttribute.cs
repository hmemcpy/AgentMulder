using System.Collections.Generic;
using JetBrains.ReSharper.TestFramework;
using SimpleInjector;

namespace AgentMulder.ReSharper.Tests.SimpleInjector.Helpers
{
    public class TestSimpleInjectorAttribute : TestReferencesAttribute
    {
#if SDK81
        public override IEnumerable<string> GetReferences(JetBrains.ProjectModel.PlatformID platformId)
#else
        public override IEnumerable<string> GetReferences()
#endif
        {
            return new[]
            {
                typeof(Container).Assembly.Location
            };
        }
    }
}
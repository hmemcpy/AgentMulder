using System.Collections.Generic;
using JetBrains.ReSharper.TestFramework;
using SimpleInjector;

namespace AgentMulder.ReSharper.Tests.SimpleInjector.Helpers
{
    public class TestSimpleInjectorAttribute : TestReferencesAttribute
    {
        public override IEnumerable<string> GetReferences(JetBrains.ProjectModel.PlatformID platformId)
        {
            return new[]
            {
                typeof(Container).Assembly.Location
            };
        }
    }
}
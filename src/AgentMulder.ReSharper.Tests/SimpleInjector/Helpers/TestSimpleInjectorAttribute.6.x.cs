using System.Collections.Generic;
using JetBrains.ReSharper.TestFramework;
using SimpleInjector;

namespace AgentMulder.ReSharper.Tests.SimpleInjector.Helpers
{
    public class TestSimpleInjectorAttribute : TestReferencesAttribute
    {
        public override string[] GetReferences()
        {
            return new[]
            {
                typeof(Container).Assembly.Location
            };
        }
    }
}
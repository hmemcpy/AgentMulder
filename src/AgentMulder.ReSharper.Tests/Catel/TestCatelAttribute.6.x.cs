using System.Collections.Generic;
using Catel.IoC;
using JetBrains.ReSharper.TestFramework;

namespace AgentMulder.ReSharper.Tests.Catel
{
    public class TestCatelAttribute : TestReferencesAttribute
    {
        public override string[] GetReferences()
        {
            return new[]
            {
                typeof(ServiceLocator).Assembly.Location
            };
        }
    }
}
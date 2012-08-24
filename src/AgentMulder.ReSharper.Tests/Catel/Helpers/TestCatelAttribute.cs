using System.Collections.Generic;
using Catel.IoC;
using JetBrains.ReSharper.TestFramework;

namespace AgentMulder.ReSharper.Tests.Catel.Helpers
{
    public class TestCatelAttribute : TestReferencesAttribute
    {
#if SDK70
        public override IEnumerable<string> GetReferences()
#else
        public override string[] GetReferences()
#endif
        {
            return new[]
            {
                typeof(ServiceLocator).Assembly.Location
            };
        }
    }
}
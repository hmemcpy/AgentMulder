using System.Collections.Generic;
using Catel.IoC;
using JetBrains.ReSharper.TestFramework;

namespace AgentMulder.ReSharper.Tests.Catel
{
    public class TestCatelAttribute : TestReferencesAttribute
    {
#if SDK81
        public override IEnumerable<string> GetReferences(JetBrains.ProjectModel.PlatformID platformId)
#else
        public override IEnumerable<string> GetReferences()
#endif
        {
            return new[]
            {
                typeof(ServiceLocator).Assembly.Location
            };
        }
    }
}
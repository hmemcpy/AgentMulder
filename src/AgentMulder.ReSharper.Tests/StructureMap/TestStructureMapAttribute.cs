using System.Collections.Generic;
using JetBrains.ReSharper.TestFramework;
using StructureMap;

namespace AgentMulder.ReSharper.Tests.StructureMap
{
    public class TestStructureMapAttribute : TestReferencesAttribute
    {
#if SDK81
        public override IEnumerable<string> GetReferences(JetBrains.ProjectModel.PlatformID platformId)
#else
        public override IEnumerable<string> GetReferences()
#endif
        {
            return new[]
            {
                typeof(IContainer).Assembly.Location
            };
        }
    }
}
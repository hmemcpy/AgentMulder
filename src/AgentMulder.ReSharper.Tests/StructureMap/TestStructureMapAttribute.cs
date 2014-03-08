using System.Collections.Generic;
using JetBrains.ReSharper.TestFramework;
using StructureMap;

namespace AgentMulder.ReSharper.Tests.StructureMap
{
    public class TestStructureMapAttribute : TestReferencesAttribute
    {
        public override IEnumerable<string> GetReferences(JetBrains.ProjectModel.PlatformID platformId)
        {
            return new[]
            {
                typeof(IContainer).Assembly.Location
            };
        }
    }
}
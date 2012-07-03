using System.Collections.Generic;
using JetBrains.ReSharper.TestFramework;
using StructureMap;

namespace AgentMulder.ReSharper.Tests.StructureMap.Helpers
{
    public class TestStructureMapAttribute : TestReferencesAttribute
    {
#if SDK70
        public override IEnumerable<string> GetReferences()
#else
        public override string[] GetReferences()
#endif
        {
            return new[]
            {
                typeof(IContainer).Assembly.Location
            };
        }
    }
}
using JetBrains.ReSharper.TestFramework;
using StructureMap;

namespace AgentMulder.ReSharper.Tests.StructureMap.Helpers
{
    public class TestStructureMapAttribute : TestReferencesAttribute
    {
        public override string[] GetReferences()
        {
            return new[]
            {
                typeof(IContainer).Assembly.Location
            };
        }
    }
}
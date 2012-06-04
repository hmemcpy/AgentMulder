using JetBrains.ReSharper.TestFramework;
using Microsoft.Practices.Unity;

namespace AgentMulder.ReSharper.Tests.Unity.Helpers
{
    public class TestUnityAttribute : TestReferencesAttribute
    {
        public override string[] GetReferences()
        {
            return new[]
            {
                typeof(IUnityContainer).Assembly.Location
            };
        }
    }
}
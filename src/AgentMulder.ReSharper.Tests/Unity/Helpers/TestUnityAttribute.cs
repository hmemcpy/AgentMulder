using System.Collections.Generic;
using JetBrains.ReSharper.TestFramework;
using Microsoft.Practices.Unity;

namespace AgentMulder.ReSharper.Tests.Unity.Helpers
{
    public class TestUnityAttribute : TestReferencesAttribute
    {
#if SDK70 || SDK80
        public override IEnumerable<string> GetReferences()
#else
        public override string[] GetReferences()
#endif
        {
            return new[]
            {
                typeof(IUnityContainer).Assembly.Location
            };
        }
    }
}
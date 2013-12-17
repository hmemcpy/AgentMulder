using System.Collections.Generic;
using JetBrains.ReSharper.TestFramework;
using Microsoft.Practices.Unity;

namespace AgentMulder.ReSharper.Tests.Unity
{
    public class TestUnityAttribute : TestReferencesAttribute
    {
#if SDK81
        public override IEnumerable<string> GetReferences(JetBrains.ProjectModel.PlatformID platformId)
#else
        public override IEnumerable<string> GetReferences()
#endif
        {
            return new[]
            {
                typeof(IUnityContainer).Assembly.Location
            };
        }
    }
}
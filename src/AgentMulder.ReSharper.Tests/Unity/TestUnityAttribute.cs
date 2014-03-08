using System.Collections.Generic;
using JetBrains.ReSharper.TestFramework;
using Microsoft.Practices.Unity;

namespace AgentMulder.ReSharper.Tests.Unity
{
    public class TestUnityAttribute : TestReferencesAttribute
    {
        public override IEnumerable<string> GetReferences(JetBrains.ProjectModel.PlatformID platformId)
        {
            return new[]
            {
                typeof(IUnityContainer).Assembly.Location
            };
        }
    }
}
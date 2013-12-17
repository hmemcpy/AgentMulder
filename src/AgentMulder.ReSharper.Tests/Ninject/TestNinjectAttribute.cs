using System.Collections.Generic;
using JetBrains.ReSharper.TestFramework;
using Ninject.Modules;

namespace AgentMulder.ReSharper.Tests.Ninject
{
    public class TestNinjectAttribute : TestReferencesAttribute
    {
#if SDK81
        public override IEnumerable<string> GetReferences(JetBrains.ProjectModel.PlatformID platformId)
#else
        public override IEnumerable<string> GetReferences()
#endif
        {
            return new[]
            {
                typeof(NinjectModule).Assembly.Location
            };
        }
    }
}
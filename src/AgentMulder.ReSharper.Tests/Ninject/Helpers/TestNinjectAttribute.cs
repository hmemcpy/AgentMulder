using JetBrains.ReSharper.TestFramework;
using Ninject.Modules;

namespace AgentMulder.ReSharper.Tests.Ninject.Helpers
{
    public class TestNinjectAttribute : TestReferencesAttribute
    {
        public override string[] GetReferences()
        {
            return new[]
            {
                typeof(NinjectModule).Assembly.Location
            };
        }
    }
}
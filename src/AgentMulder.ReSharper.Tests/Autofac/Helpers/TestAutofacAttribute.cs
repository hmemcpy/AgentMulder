using Autofac;
using JetBrains.ReSharper.TestFramework;

namespace AgentMulder.ReSharper.Tests.Autofac.Helpers
{
    public class TestAutofacAttribute : TestReferencesAttribute
    {
        public override string[] GetReferences()
        {
            return new[]
            {
                typeof(ContainerBuilder).Assembly.Location
            };
        }
    }
}
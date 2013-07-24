using System.Collections.Generic;
using Autofac;
using JetBrains.ReSharper.TestFramework;

namespace AgentMulder.ReSharper.Tests.Autofac.Helpers
{
    public class TestAutofacAttribute : TestReferencesAttribute
    {
#if SDK70 || SDK80
        public override IEnumerable<string> GetReferences()
#else
        public override string[] GetReferences()
#endif
        {
            return new[]
            {
                typeof(ContainerBuilder).Assembly.Location
            };
        }
    }
}
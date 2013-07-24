using System.Collections.Generic;
using Castle.Core;
using Castle.Windsor;
using JetBrains.ReSharper.TestFramework;

namespace AgentMulder.ReSharper.Tests.Windsor.Helpers
{
    public class TestWindsorAttribute : TestReferencesAttribute
    {
#if SDK70 || SDK80
        public override IEnumerable<string> GetReferences()
#else
        public override string[] GetReferences()
#endif
        {
            return new[]
            {
                typeof(IServiceEnabledComponent).Assembly.Location, // Castle.Core.dll
                typeof(WindsorContainer).Assembly.Location // Castle.Windsor.dll
            };
        }
    }
}
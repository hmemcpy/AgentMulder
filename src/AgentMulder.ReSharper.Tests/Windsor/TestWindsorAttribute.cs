using System.Collections.Generic;
using Castle.Core;
using Castle.Windsor;
using JetBrains.ReSharper.TestFramework;

namespace AgentMulder.ReSharper.Tests.Windsor
{
    public class TestWindsorAttribute : TestReferencesAttribute
    {
        public override IEnumerable<string> GetReferences()
        {
            return new[]
            {
                typeof(IServiceEnabledComponent).Assembly.Location, // Castle.Core.dll
                typeof(WindsorContainer).Assembly.Location // Castle.Windsor.dll
            };
        }
    }
}
using Castle.Core;
using Castle.Windsor;
using JetBrains.ReSharper.TestFramework;

namespace AgentMulder.ReSharper.Tests.Windsor.Helpers
{
    public class TestWindsorAttribute : TestReferencesAttribute
    {
        public override string[] GetReferences()
        {
            return new[]
            {
                typeof(IServiceEnabledComponent).Assembly.Location, // Castle.Core.dll
                typeof(WindsorContainer).Assembly.Location // Castle.Windsor.dll
            };
        }
    }
}
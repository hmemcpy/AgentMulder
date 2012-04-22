using System.Reflection;
using Castle.Core;
using Castle.Windsor;
using JetBrains.ReSharper.TestFramework;

namespace AgentMulder.ReSharper.Tests.Windsor
{
    public class TestWindsorAttribute : TestReferencesAttribute
    {
        public override string[] GetReferences()
        {
            return new[]
            {
                Assembly.GetExecutingAssembly().Location,
                typeof(IServiceEnabledComponent).Assembly.Location, // Castle.Core.dll
                typeof(WindsorContainer).Assembly.Location // Castle.Windsor.dll
            };
        }
    }
}
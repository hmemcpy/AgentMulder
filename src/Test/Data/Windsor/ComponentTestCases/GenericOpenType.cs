// Patterns: 1
// Matches: MyList.cs
// NotMatches: Foo.cs

using System.Collections.Generic;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using TestApplication.Types;

namespace TestApplication.Windsor.ComponentTestCases
{
    public class GenericOpenType : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For(typeof(IEnumerable<>)).ImplementedBy(typeof(MyList<>)));
        }
    }
}
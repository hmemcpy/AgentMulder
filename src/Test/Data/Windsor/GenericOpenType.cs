using System.Collections.Generic;
using AgentMulder.ReSharper.Tests.Data;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace TestApplication.Windsor
{
    public class GenericOpenType : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For(typeof(IEnumerable<>)).ImplementedBy(typeof(MyList<>)));
        }
    }
}
using AgentMulder.Containers.Ninject;
using AgentMulder.Containers.Ninject.Providers;
using AgentMulder.ReSharper.Domain.Containers;
using AgentMulder.ReSharper.Tests.Ninject.Helpers;

namespace AgentMulder.ReSharper.Tests.Ninject
{
    [TestNinject]
    public class ModuleTests : AgentMulderTestBase
    {
        protected override string RelativeTestDataPath
        {
            get { return @"Ninject\ModuleTestCases"; }
        }

        protected override IContainerInfo ContainerInfo
        {
            get
            {
                return new NinjectContainerInfo(new[]
                {
                    new BindRegistrationProvider(new ToRegistrationProvider())
                });
            }
        }
    }
}
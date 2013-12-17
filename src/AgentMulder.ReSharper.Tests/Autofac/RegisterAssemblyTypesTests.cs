using AgentMulder.Containers.Autofac;
using AgentMulder.ReSharper.Domain.Containers;

namespace AgentMulder.ReSharper.Tests.Autofac
{
    [TestAutofac]
    public class RegisterAssemblyTypesTests : AgentMulderTestBase
    {
        protected override string RelativeTestDataPath
        {
            get { return @"Autofac\RegisterAssemblyTypesTests"; }
        }

        protected override IContainerInfo ContainerInfo
        {
            get { return new AutofacContainerInfo(); }
        }
    }
}
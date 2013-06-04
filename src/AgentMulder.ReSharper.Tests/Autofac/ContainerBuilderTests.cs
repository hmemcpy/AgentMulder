using AgentMulder.Containers.Autofac;
using AgentMulder.ReSharper.Domain.Containers;
using AgentMulder.ReSharper.Tests.Autofac.Helpers;

namespace AgentMulder.ReSharper.Tests.Autofac
{
    [TestAutofac]
    public class ContainerBuilderTests : AgentMulderTestBase
    {
        protected override string RelativeTestDataPath
        {
            get { return @"Autofac"; }
        }

        protected override IContainerInfo ContainerInfo
        {
            get { return new AutofacContainerInfo(); }
        }
    }
}
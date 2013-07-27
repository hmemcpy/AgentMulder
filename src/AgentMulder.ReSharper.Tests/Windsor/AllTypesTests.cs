using AgentMulder.Containers.CastleWindsor;
using AgentMulder.Containers.CastleWindsor.Providers;
using AgentMulder.ReSharper.Domain.Containers;

namespace AgentMulder.ReSharper.Tests.Windsor
{
    [TestWindsor]
    public class AllTypesTests : AgentMulderTestBase
    {
        protected override string RelativeTestDataPath
        {
            get { return @"Windsor\TestCases"; }
        }

        protected override IContainerInfo ContainerInfo
        {
            get
            {
                return new WindsorContainerInfo(new[]
                {
                    new AllTypesRegistrationProvider(new BasedOnRegistrationProvider())
                });
            }
        }
    }
}
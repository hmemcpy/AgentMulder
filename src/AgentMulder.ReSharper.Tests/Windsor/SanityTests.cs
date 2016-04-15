using AgentMulder.Containers.CastleWindsor;
using AgentMulder.Containers.CastleWindsor.Providers;
using AgentMulder.ReSharper.Domain.Containers;

namespace AgentMulder.ReSharper.Tests.Windsor
{
    [TestWithNuGetPackage(Packages = new[] { "Castle.Windsor/3.3.0", "Castle.Core/3.3.0" })]
    public class SanityTests : AgentMulderTestBase<WindsorContainerInfo>
    {
        protected override string RelativeTestDataPath
        {
            get { return @"Windsor"; }
        }

        protected override IContainerInfo ContainerInfo
        {
            get
            {
                return new WindsorContainerInfo(new[]
                {
                    new ClassesRegistrationProvider(new BasedOnRegistrationProvider())
                });
            }
        }
    }
}
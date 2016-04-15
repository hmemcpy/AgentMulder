using System;
using AgentMulder.Containers.CastleWindsor;
using AgentMulder.Containers.CastleWindsor.Providers;
using AgentMulder.ReSharper.Domain.Containers;

namespace AgentMulder.ReSharper.Tests.Windsor
{
    [TestWithNuGetPackage(Packages = new[] { "Castle.Windsor/3.3.0", "Castle.Core/3.3.0" })]
    public class ComponentRegistrationTests : AgentMulderTestBase<WindsorContainerInfo>
    {
        protected override string RelativeTestDataPath
        {
            get { return @"Windsor\ComponentTestCases"; }
        }

        protected override IContainerInfo ContainerInfo
        {
            get
            {
                return new WindsorContainerInfo(new[]
                {
                    new ComponentRegistrationProvider(new ImplementedByRegistrationProvider())
                });
            }
        }
    }
}
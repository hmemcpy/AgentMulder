using AgentMulder.Containers.Catel;

namespace AgentMulder.ReSharper.Tests.Catel
{
    [TestWithNuGetPackage(Packages = new[] { "Catel.Core" })]
    public class ServiceLocatorTests : AgentMulderTestBase<CatelContainerInfo>
    {
        protected override string RelativeTestDataPath
        {
            get { return @"Catel"; }
        }
    }
}
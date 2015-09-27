using AgentMulder.Containers.Catel;

namespace AgentMulder.ReSharper.Tests.Catel
{
    [TestWithNuGetPackage(Packages = new[] { "Catel.Core:3.9.0" })]
    public class ServiceLocatorTests : AgentMulderTestBase<CatelContainerInfo>
    {
        protected override string RelativeTestDataPath
        {
            get { return @"Catel"; }
        }
    }
}
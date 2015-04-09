using AgentMulder.Containers.Ninject;

namespace AgentMulder.ReSharper.Tests.Ninject
{
    [TestWithNuGetPackage(Packages = new[] { "Ninject:3.2.2.0" })]
    public class KernelTests : AgentMulderTestBase<NinjectContainerInfo>
    {
        protected override string RelativeTestDataPath
        {
            get { return @"Ninject\KernelTestCases"; }
        }
    }
}
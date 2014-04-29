using AgentMulder.Containers.Ninject;

namespace AgentMulder.ReSharper.Tests.Ninject
{
    [TestWithNuGetPackage(Packages = new[] { "Ninject" })]
    public class ModuleTests : AgentMulderTestBase<NinjectContainerInfo>
    {
        protected override string RelativeTestDataPath
        {
            get { return @"Ninject\ModuleTestCases"; }
        }
    }
}
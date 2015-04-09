using AgentMulder.Containers.SimpleInjector;

namespace AgentMulder.ReSharper.Tests.SimpleInjector
{
    [TestWithNuGetPackage(Packages = new[] { "SimpleInjector:2.5.2" })]
    public class ContainerTests : AgentMulderTestBase<SimpleInjectorContainerInfo>
    {
        protected override string RelativeTestDataPath
        {
            get { return @"SimpleInjector"; }
        }
    }
}
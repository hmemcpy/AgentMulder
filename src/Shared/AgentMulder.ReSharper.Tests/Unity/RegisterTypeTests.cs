using AgentMulder.Containers.Unity;

namespace AgentMulder.ReSharper.Tests.Unity
{
    [TestWithNuGetPackage(Packages = new[] { "Unity:3.5.1404.0" })]
    public class RegisterTypeTests : AgentMulderTestBase<UnityContainerInfo>
    {
        protected override string RelativeTestDataPath
        {
            get { return @"Unity"; }
        }
    }
}
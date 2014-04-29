using AgentMulder.Containers.Unity;

namespace AgentMulder.ReSharper.Tests.Unity
{
    [TestWithNuGetPackage(Packages = new[] { "Unity" })]
    public class RegisterTypeTests : AgentMulderTestBase<UnityContainerInfo>
    {
        protected override string RelativeTestDataPath
        {
            get { return @"Unity"; }
        }
    }
}
using AgentMulder.Containers.Autofac;

namespace AgentMulder.ReSharper.Tests.Autofac
{
    [TestWithNuGetPackage(Packages = new[] { "Autofac:3.5.2" })]
    public class RegisterAssemblyTypesTests : AgentMulderTestBase<AutofacContainerInfo>
    {
        protected override string RelativeTestDataPath
        {
            get { return @"Autofac\RegisterAssemblyTypesTests"; }
        }
    }
}
using AgentMulder.Containers.Autofac;

namespace AgentMulder.ReSharper.Tests.Autofac
{
    [TestWithNuGetPackage(Packages = new[] { "Autofac:3.5.2", "Autofac.Mvc5:3.3.3", "Microsoft.AspNet.WebApi.Core:5.2.2" })]
    public class MvcTests : AgentMulderTestBase<AutofacContainerInfo>
    {
        protected override string RelativeTestDataPath
        {
            get { return @"Autofac\Mvc"; }
        }
    }
}
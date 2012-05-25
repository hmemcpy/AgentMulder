using System.Linq;
using AgentMulder.Containers.Ninject;
using AgentMulder.Containers.Ninject.Providers;
using AgentMulder.ReSharper.Domain.Containers;
using NUnit.Framework;

namespace AgentMulder.ReSharper.Tests.Ninject
{
    public class SanityTests : ComponentRegistrationsTestBase
    {
        protected override string RelativeTestDataPath
        {
            get { return @"Ninject"; }
        }

        protected override IContainerInfo ContainerInfo
        {
            get
            {
                return new NinjectContainerInfo(new[]
                {
                    new BindRegistrationProvider(new ToRegistrationProvider())
                });
            }
        }

        [Test]
        public void BrokenReference_DoesNotCreateARegistration()
        {
            RunTest("_DoesNotCompile-BrokenReference", registrations =>
                Assert.AreEqual(0, registrations.Count()));
        }

        [TestCase("MethodNamedBindOnSomeClass")]
        [TestCase("MethodNamedBindInThisClass")]
        public void MethodNamedBind_DoesNotBelongToNinject_DoesNotCreateARegistration(string testName)
        {
            RunTest(testName, registrations =>
                Assert.AreEqual(0, registrations.Count()));
        }
    }
}
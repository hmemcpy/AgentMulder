using System.Linq;
using AgentMulder.Containers.CastleWindsor;
using AgentMulder.Containers.CastleWindsor.Providers;
using AgentMulder.ReSharper.Domain.Containers;
using NUnit.Framework;

namespace AgentMulder.ReSharper.Tests.Windsor
{
    public class SanityTests : ComponentRegistrationsTestBase
    {
        protected override string RelativeTestDataPath
        {
            get { return @"Windsor"; }
        }

        protected override IContainerInfo ContainerInfo
        {
            get
            {
                return new WindsorContainerInfo(new[]
                {
                    new ClassesRegistrationProvider(new BasedOnRegistrationProvider())
                });
            }
        }

        [Test]
        public void BrokenReference_DoesNotCreateARegistration()
        {
            RunTest("_DoesNotCompile-BrokenReference", registrations =>
                Assert.AreEqual(0, registrations.Count()));
        }
    }
}
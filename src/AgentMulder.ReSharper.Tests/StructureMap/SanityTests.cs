using System.Linq;
using AgentMulder.Containers.Ninject;
using AgentMulder.Containers.Ninject.Providers;
using AgentMulder.Containers.StructureMap;
using AgentMulder.ReSharper.Domain.Containers;
using NUnit.Framework;

namespace AgentMulder.ReSharper.Tests.StructureMap
{
    public class SanityTests : AgentMulderTestBase
    {
        protected override string RelativeTestDataPath
        {
            get { return @"StructureMap"; }
        }

        protected override IContainerInfo ContainerInfo
        {
            get { return new StructureMapContainerInfo(); }
        }

        [Test]
        public void BrokenReference_DoesNotCreateARegistration()
        {
            RunTest("_DoesNotCompile-BrokenReference", registrations =>
                Assert.AreEqual(0, registrations.Count()));
        }

        [TestCase("MethodNamedForOnSomeClass")]
        [TestCase("MethodNamedForInThisClass")]
        public void MethodNamedFor_DoesNotBelongToStructureMap_DoesNotCreateARegistration(string testName)
        {
            RunTest(testName, registrations =>
                Assert.AreEqual(0, registrations.Count()));
        }
    }
}
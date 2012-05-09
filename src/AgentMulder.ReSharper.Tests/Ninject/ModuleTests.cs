using System.Collections.Generic;
using AgentMulder.Containers.Ninject;
using AgentMulder.ReSharper.Domain.Containers;
using AgentMulder.ReSharper.Domain.Registrations;

namespace AgentMulder.ReSharper.Tests.Ninject
{
    public class ModuleTests : ComponentRegistrationsTestBase
    {
        // The source files are located in the solution directory, under Test\Data and the path below, i.e. Test\Data\StructuralSearch\Windsor
        // These files are loaded into the test solution that is being created by this test fixture
        protected override string RelativeTestDataPath
        {
            get { return @"Ninject\ModuleTestCases"; }
        }

        protected override string RelativeTypesPath
        {
            get { return @"..\..\Types"; }
        }

        protected override IContainerInfo ContainerInfo
        {
            get { return new NinjectContainerInfo(new IRegistrationPatternsProvider[0]); }
        }
    }
}
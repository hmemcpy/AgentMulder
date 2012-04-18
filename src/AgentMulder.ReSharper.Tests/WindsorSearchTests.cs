using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.StructuralSearchTests.CSharp;
using NUnit.Framework;

namespace AgentMulder.ReSharper.Tests
{
    public class WindsorSearchTests : CSharpStructuralSearchBaseTest
    {
        // The "Gold" files are located in the solution directory, under Test\Data and the path below, i.e. Test\Data\StructuralSearch\Windsor
        // Each file is named after the test name, having .gold extension
        protected override string RelativeTestDataPath
        {
            get { return @"StructuralSearch\Windsor"; }
        }

        [Test]
        public void Test01()
        {
            TestOnePattern("Component.For<$interface$>().ImplementedBy<$concrete$>()", "Test01.cs", new TypePlaceholder("interface"), new TypePlaceholder("concrete"));
        }
    }
}
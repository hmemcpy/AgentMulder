using System;
#if SDK90
using JetBrains.ReSharper.TestFramework;
#else
using JetBrains.ReSharper.PsiTests.Asp;
#endif

namespace AgentMulder.ReSharper.Tests
{
    public class TestWithNuGetPackageAttribute : TestPackagesAttribute
    {
#if SDK90
        private string[] Sources 
        {
            get { return Packages; }
            set { Packages = value; }
        }
#endif

        public TestWithNuGetPackageAttribute()
        {
            Sources = new[] { @"%BASE_TEST_DATA%\..\..\packages" };
        }
    }
}
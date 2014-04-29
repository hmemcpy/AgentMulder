using System;
using JetBrains.ReSharper.PsiTests.Asp;

namespace AgentMulder.ReSharper.Tests
{
    public class TestWithNuGetPackageAttribute : TestPackagesAttribute
    {
        public TestWithNuGetPackageAttribute()
        {
            Sources = new[] { @"%BASE_TEST_DATA%\..\..\packages" };
        }
    }
}
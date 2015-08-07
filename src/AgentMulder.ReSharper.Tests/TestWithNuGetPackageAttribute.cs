using System;

using JetBrains.ReSharper.TestFramework;

namespace AgentMulder.ReSharper.Tests
{
    public class TestWithNuGetPackageAttribute : TestPackagesAttribute
    {
        public TestWithNuGetPackageAttribute()
        {
            Packages = new[] { @"%BASE_TEST_DATA%\..\..\packages" };
        }
    }
}
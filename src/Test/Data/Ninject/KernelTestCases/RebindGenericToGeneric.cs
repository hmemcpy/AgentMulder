// Patterns: 1
// Matches: CommonImpl1.cs
// NotMatches: Foo.cs

using Ninject;
using TestApplication.Types;

namespace TestApplication.Ninject.KernelTestCases
{
    public class RebindGenericToGeneric
    {
        public RebindGenericToGeneric()
        {
            var kernel = new StandardKernel();
            kernel.Rebind<ICommon>().To<CommonImpl1>();
        }
    }
}
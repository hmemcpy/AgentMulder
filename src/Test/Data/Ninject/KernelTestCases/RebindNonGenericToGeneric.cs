// Patterns: 1
// Matches: CommonImpl1.cs
// NotMatches: Foo.cs

using Ninject;
using TestApplication.Types;

namespace TestApplication.Ninject.KernelTestCases
{
    public class RebindNonGenericToGeneric
    {
        public RebindNonGenericToGeneric()
        {
            var kernel = new StandardKernel();
            kernel.Rebind(typeof(ICommon)).To<CommonImpl1>();
        }
    }
}
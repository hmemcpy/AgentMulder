// Patterns: 1
// Matches: CommonImpl1.cs
// NotMatches: Foo.cs

using Ninject;
using TestApplication.Types;

namespace TestApplication.Ninject.KernelTestCases
{
    public class RebindNonGenericToNonGeneric
    {
        public RebindNonGenericToNonGeneric()
        {
            var kernel = new StandardKernel();
            kernel.Rebind(typeof(ICommon)).To(typeof(CommonImpl1));
        }
    }
}
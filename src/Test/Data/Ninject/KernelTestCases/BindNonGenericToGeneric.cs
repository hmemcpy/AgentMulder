// Patterns: 1
// Matches: CommonImpl1.cs
// NotMatches: Foo.cs

using Ninject;
using TestApplication.Types;

namespace TestApplication.Ninject.KernelTestCases
{
    public class BindNonGenericToGeneric
    {
        public BindNonGenericToGeneric()
        {
            var kernel = new StandardKernel();
            kernel.Bind(typeof(ICommon)).To<CommonImpl1>();
        }
    }
}
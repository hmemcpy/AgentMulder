// Patterns: 1
// Matches: CommonImpl1.cs
// NotMatches: Foo.cs

using Ninject;
using TestApplication.Types;

namespace TestApplication.Ninject.KernelTestCases
{
    public class BindGenericToNonGeneric
    {
        public BindGenericToNonGeneric()
        {
            var kernel = new StandardKernel();
            kernel.Bind<ICommon>().To(typeof(CommonImpl1));
        }
    }
}
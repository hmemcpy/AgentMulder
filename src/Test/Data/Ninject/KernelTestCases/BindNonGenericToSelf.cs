// Patterns: 1
// Matches: CommonImpl1.cs
// NotMatches: Foo.cs

using Ninject;
using TestApplication.Types;

namespace TestApplication.Ninject.KernelTestCases
{
    public class BindNonGenericToSelf
    {
        public BindNonGenericToSelf()
        {
            var kernel = new StandardKernel();
            kernel.Bind(typeof(CommonImpl1)).ToSelf();
        }
    }
}
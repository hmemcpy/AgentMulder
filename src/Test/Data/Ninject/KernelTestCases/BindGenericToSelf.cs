// Patterns: 1
// Matches: CommonImpl1.cs
// NotMatches: Foo.cs

using Ninject;
using TestApplication.Types;

namespace TestApplication.Ninject.KernelTestCases
{
    public class BindGenericToSelf
    {
        public BindGenericToSelf()
        {
            var kernel = new StandardKernel();
            kernel.Bind<CommonImpl1>().ToSelf();
        }
    }
}
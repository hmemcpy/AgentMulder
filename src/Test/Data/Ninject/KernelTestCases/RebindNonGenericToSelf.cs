// Patterns: 1
// Matches: CommonImpl1.cs
// NotMatches: Foo.cs

using Ninject;
using TestApplication.Types;

namespace TestApplication.Ninject.KernelTestCases
{
    public class RebindNonGenericToSelf
    {
        public RebindNonGenericToSelf()
        {
            var kernel = new StandardKernel();
            kernel.Rebind(typeof(CommonImpl1)).ToSelf();
        }
    }
}
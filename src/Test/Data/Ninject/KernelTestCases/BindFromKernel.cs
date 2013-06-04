// Patterns: 1
// Matches: PageRepository.cs
// NotMatches: Foo.cs

using Ninject;
using TestApplication.Types;

namespace TestApplication.Ninject.KernelTestCases
{
    public class BindFromKernel
    {
        public BindFromKernel()
        {
            var kernel = new StandardKernel();
            Register(kernel);
        }

        private static void Register(IKernel kernel)
        {
            kernel.Bind<IPageRepository>().To<PageRepository>();
        }
    }
}
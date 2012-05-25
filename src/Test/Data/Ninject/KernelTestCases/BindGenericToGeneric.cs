using Ninject;
using TestApplication.Types;

namespace TestApplication.Ninject.KernelTestCases
{
    public class BindGenericToGeneric
    {
        public BindGenericToGeneric()
        {
            var kernel = new StandardKernel();
            kernel.Bind<ICommon>().To<CommonImpl1>();
        }
    }
}
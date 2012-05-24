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
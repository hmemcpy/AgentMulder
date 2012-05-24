using Ninject;
using Ninject.Modules;
using TestApplication.Types;

namespace TestApplication.Ninject.KernelTestCases
{
    public class BindNonGenericToNonGeneric
    {
        public BindNonGenericToNonGeneric()
        {
            var kernel = new StandardKernel();
            kernel.Bind(typeof(ICommon)).To(typeof(CommonImpl1));
        }
    }
}
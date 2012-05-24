using Ninject;
using Ninject.Modules;
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
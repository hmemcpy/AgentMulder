using Ninject;
using TestApplication.Types;

namespace TestApplication.Ninject.KernelTestCases
{
    public class RebindGenericToSelf
    {
        public RebindGenericToSelf()
        {
            var kernel = new StandardKernel();
            kernel.Rebind<CommonImpl1>().ToSelf();
        }
    }
}
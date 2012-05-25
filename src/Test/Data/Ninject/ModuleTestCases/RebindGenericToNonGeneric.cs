using Ninject.Modules;
using TestApplication.Types;

namespace TestApplication.Ninject.ModuleTestCases
{
    public class RebindGenericToNonGeneric : NinjectModule
    {
        public override void Load()
        {
            Rebind<ICommon>().To(typeof(CommonImpl1));
        }
    }
}
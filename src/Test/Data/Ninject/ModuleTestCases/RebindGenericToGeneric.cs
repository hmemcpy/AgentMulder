using Ninject;
using Ninject.Modules;
using TestApplication.Types;

namespace TestApplication.Ninject.ModuleTestCases
{
    public class RebindGenericToGeneric : NinjectModule
    {
        public override void Load()
        {
            Rebind<ICommon>().To<CommonImpl1>();
        }
    }
}
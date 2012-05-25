using Ninject.Modules;
using TestApplication.Types;

namespace TestApplication.Ninject.ModuleTestCases
{
    public class RebindNonGenericToGeneric : NinjectModule
    {
        public override void Load()
        {
            Rebind(typeof(ICommon)).To<CommonImpl1>();
        }
    }
}
using Ninject;
using Ninject.Modules;
using TestApplication.Types;

namespace TestApplication.Ninject.ModuleTestCases
{
    public class RebindNonGenericToNonGeneric : NinjectModule
    {
        public override void Load()
        {
            Rebind(typeof(ICommon)).To(typeof(CommonImpl1));
        }
    }
}
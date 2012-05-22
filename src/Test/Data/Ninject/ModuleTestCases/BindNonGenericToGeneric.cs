using Ninject.Modules;
using TestApplication.Types;

namespace TestApplication.Ninject.ModuleTestCases
{
    public class BindNonGenericToGeneric : NinjectModule
    {
        public override void Load()
        {
            Bind(typeof(ICommon)).To<CommonImpl1>();
        }
    }
}
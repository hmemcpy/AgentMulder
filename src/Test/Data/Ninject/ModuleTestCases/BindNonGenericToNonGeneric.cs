using Ninject.Modules;
using TestApplication.Types;

namespace TestApplication.Ninject.ModuleTestCases
{
    public class BindNonGenericToNonGeneric : NinjectModule
    {
        public override void Load()
        {
            Bind(typeof(ICommon)).To(typeof(CommonImpl1));
        }
    }
}
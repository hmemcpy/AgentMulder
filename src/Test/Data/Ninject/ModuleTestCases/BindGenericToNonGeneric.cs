using Ninject.Modules;
using TestApplication.Types;

namespace TestApplication.Ninject.ModuleTestCases
{
    public class BindGenericToNonGeneric : NinjectModule
    {
        public override void Load()
        {
            Bind<ICommon>().To(typeof(CommonImpl1));
        }
    }
}
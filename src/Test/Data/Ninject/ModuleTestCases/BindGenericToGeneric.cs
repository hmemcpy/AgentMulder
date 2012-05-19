using Ninject.Modules;
using TestApplication.Types;

namespace TestApplication.Ninject.ModuleTestCases
{
    public class BindGenericToGeneric : NinjectModule
    {
        public override void Load()
        {
            Bind<ICommon>().To<CommonImpl1>();
        }
    }
}
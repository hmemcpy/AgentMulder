using Ninject.Modules;
using TestApplication.Types;

namespace TestApplication.Ninject.ModuleTestCases
{
    public class Bind2GenericToGeneric : NinjectModule
    {
        public override void Load()
        {
            Bind<ICommon, ICommon2>().To<CommonImpl12>();
        }
    }
}
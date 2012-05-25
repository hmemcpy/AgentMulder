using Ninject.Modules;
using TestApplication.Types;

namespace TestApplication.Ninject.ModuleTestCases
{
    public class BindGenericToSelf : NinjectModule
    {
        public override void Load()
        {
            Bind<CommonImpl1>().ToSelf();
        }
    }
}
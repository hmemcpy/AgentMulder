using Ninject.Modules;
using TestApplication.Types;

namespace TestApplication.Ninject.ModuleTestCases
{
    public class RebindGenericToSelf : NinjectModule
    {
        public override void Load()
        {
            Rebind<CommonImpl1>().ToSelf();
        }
    }
}
using Ninject.Modules;
using TestApplication.Types;

namespace TestApplication.Ninject.ModuleTestCases
{
    public class RebindNonGenericToSelf : NinjectModule
    {
        public override void Load()
        {
            Rebind(typeof(CommonImpl1)).ToSelf();
        }
    }
}
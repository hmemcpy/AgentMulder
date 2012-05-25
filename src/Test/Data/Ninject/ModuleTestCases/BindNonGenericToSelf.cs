using Ninject;
using Ninject.Modules;
using TestApplication.Types;

namespace TestApplication.Ninject.ModuleTestCases
{
    public class BindNonGenericToSelf : NinjectModule
    {
        public override void Load()
        {
            Bind(typeof(CommonImpl1)).ToSelf();
        }
    }
}
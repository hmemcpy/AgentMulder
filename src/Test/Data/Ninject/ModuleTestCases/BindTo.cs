using Ninject.Modules;
using TestApplication.Types;

namespace TestApplication.Ninject.ModuleTestCases
{
    public class BindTo : NinjectModule
    {
        public override void Load()
        {
            Bind<IFoo>().To<Foo>();
        }
    }
}
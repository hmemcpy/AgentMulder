using Ninject.Modules;
using TestApplication.Types;

namespace TestApplication.Ninject.ModuleTestCases
{
    public class Bind1GenericToGeneric : NinjectModule
    {
        public override void Load()
        {
            Bind<IFoo>().To<Foo>();
        }
    }
}
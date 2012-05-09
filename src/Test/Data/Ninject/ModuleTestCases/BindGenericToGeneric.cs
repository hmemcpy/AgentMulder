using Ninject.Modules;
using Ninject.Syntax;
using TestApplication.Types;

namespace TestApplication.Ninject.ModuleTestCases
{
    public class BindGenericToGeneric : NinjectModule
    {
        public override void Load()
        {
            Bind<IFoo>().To<Foo>();
        }
    }
}
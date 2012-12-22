// Patterns: 1
// Matches: CommonImpl1.cs
// NotMatches: Foo.cs

using Ninject.Modules;
using TestApplication.Types;

namespace TestApplication.Ninject.ModuleTestCases
{
    public class ModuleRebindGenericToGeneric : NinjectModule
    {
        public override void Load()
        {
            Rebind<ICommon>().To<CommonImpl1>();
        }
    }
}
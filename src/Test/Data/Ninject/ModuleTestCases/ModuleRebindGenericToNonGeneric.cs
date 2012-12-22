// Patterns: 1
// Matches: CommonImpl1.cs
// NotMatches: Foo.cs

using Ninject.Modules;
using TestApplication.Types;

namespace TestApplication.Ninject.ModuleTestCases
{
    public class ModuleRebindGenericToNonGeneric : NinjectModule
    {
        public override void Load()
        {
            Rebind<ICommon>().To(typeof(CommonImpl1));
        }
    }
}
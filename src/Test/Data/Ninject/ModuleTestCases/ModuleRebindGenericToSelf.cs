// Patterns: 1
// Matches: CommonImpl1.cs
// NotMatches: Foo.cs

using Ninject.Modules;
using TestApplication.Types;

namespace TestApplication.Ninject.ModuleTestCases
{
    public class ModuleRebindGenericToSelf : NinjectModule
    {
        public override void Load()
        {
            Rebind<CommonImpl1>().ToSelf();
        }
    }
}
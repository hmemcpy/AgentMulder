// Patterns: 1
// Matches: CommonImpl1.cs
// NotMatches: Foo.cs

using Ninject.Modules;
using TestApplication.Types;

namespace TestApplication.Ninject.ModuleTestCases
{
    public class ModuleRebindNonGenericToSelf : NinjectModule
    {
        public override void Load()
        {
            Rebind(typeof(CommonImpl1)).ToSelf();
        }
    }
}
// Patterns: 1
// Matches: CommonImpl1.cs
// NotMatches: Foo.cs

using Ninject.Modules;
using TestApplication.Types;

namespace TestApplication.Ninject.ModuleTestCases
{
    public class ModuleBindGenericToSelf : NinjectModule
    {
        public override void Load()
        {
            Bind<CommonImpl1>().ToSelf();
        }
    }
}
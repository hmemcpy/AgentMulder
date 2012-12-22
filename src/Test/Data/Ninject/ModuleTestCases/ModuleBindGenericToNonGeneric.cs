// Patterns: 1
// Matches: CommonImpl1.cs
// NotMatches: Foo.cs

using Ninject.Modules;
using TestApplication.Types;

namespace TestApplication.Ninject.ModuleTestCases
{
    public class ModuleBindGenericToNonGeneric : NinjectModule
    {
        public override void Load()
        {
            Bind<ICommon>().To(typeof(CommonImpl1));
        }
    }
}
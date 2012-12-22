// Patterns: 1
// Matches: MyList.cs
// NotMatches: Foo.cs

using System.Collections.Generic;
using Ninject.Modules;
using TestApplication.Types;

namespace TestApplication.Ninject.ModuleTestCases
{
    public class ModuleGenericOpenType : NinjectModule
    {
        public override void Load()
        {
            Bind(typeof(IEnumerable<>)).To(typeof(MyList<>));
        }
    }
}
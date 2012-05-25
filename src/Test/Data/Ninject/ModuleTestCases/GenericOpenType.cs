using System.Collections.Generic;
using Ninject;
using Ninject.Modules;
using TestApplication.Types;

namespace TestApplication.Ninject.ModuleTestCases
{
    public class GenericOpenType : NinjectModule
    {
        public override void Load()
        {
            Bind(typeof(IEnumerable<>)).To(typeof(MyList<>));
        }
    }
}
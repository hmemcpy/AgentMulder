using System.Collections.Generic;
using Ninject;
using TestApplication.Types;

namespace TestApplication.Ninject.KernelTestCases
{
    public class GenericOpenType
    {
        public GenericOpenType()
        {
            var kernel = new StandardKernel();
            kernel.Bind(typeof(IEnumerable<>)).To(typeof(MyList<>));
        }
    }
}
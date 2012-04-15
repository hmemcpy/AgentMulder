using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace TestApplication.Mef
{
    [Export(typeof(IService))]
    public class Service : IService
    {
        [ImportMany(typeof(IDependency))]
        public IEnumerable<IDependency> Dependencies { get; private set; }
    }
}
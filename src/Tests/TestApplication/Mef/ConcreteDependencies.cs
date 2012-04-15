using System.ComponentModel.Composition;

namespace TestApplication.Mef
{
    [Export(typeof(IDependency))]
    public class ConcreteDependency1 : IDependency
    {
         
    }

    [Export(typeof(IDependency))]
    public class ConcreteDependency2 : IDependency
    {

    }
}
using System;
using AgentMulder.TestCases;

namespace TestApplication
{
    static class Program
    {
        static void Main(string[] args)
        {
            var pm = new Service();
            var resolve = pm.container.Resolve<Service.IFoo>();
        }
    }
}

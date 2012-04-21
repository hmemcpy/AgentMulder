using System;
using AgentMulder.TestApplication;
using TestApplication.Windsor;

namespace TestApplication
{
    static class Program
    {
        static void Main(string[] args)
        {
            var pm = new WindsorRegistration();
            var resolve = pm.container.Resolve<IFoo>();
        }
    }
}

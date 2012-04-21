using System;
using TestApplication.Windsor;

namespace TestApplication
{
    static class Program
    {
        static void Main(string[] args)
        {
            var pm = new WindsorRegistration();
            var resolve = pm.container.Resolve<WindsorRegistration.IFoo>();
        }
    }
}

using AgentMulder.ReSharper.Tests.Data;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace TestApplication
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var container = new WindsorContainer();
            container.Register(AllTypes.FromAssemblyContaining<IFoo>());


        }
    }
}
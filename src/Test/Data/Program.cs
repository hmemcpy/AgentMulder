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
            container.Register(Types.From(typeof(Baz), typeof(MyStruct)).Pick());

            var baz = container.Resolve<Baz>();
            var s = container.Resolve<MyStruct>();
        }
    }
}
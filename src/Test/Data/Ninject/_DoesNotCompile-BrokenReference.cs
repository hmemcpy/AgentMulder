// Patterns: 0

using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Ninject;

namespace TestApplication.Ninject
{
    // THIS FILE IS NOT BEING COMPILED, AND IS SET TO BE IGNORED BY RESHARPER
    // this file tests what happens if the plugin tries to analyze unresolved types
    public class BrokenReference
    {
        public BrokenReference()
        {
            var kernel = new StandardKernel();
            kernel.Bind<IDontExist>().To<DontExistImplementation>();
        }
    }
}
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Reflection;
using AgentMulder.ReSharper.Domain.Containers;

namespace AgentMulder.Containers.SimpleInjector
{
    public class SimpleInjectorContainerInfo : ContainerInfoBase
    {
        public override IEnumerable<string> ContainerQualifiedNames
        {
            get
            {
                yield return "SimpleInjector";
            }
        }

        public override string ContainerDisplayName
        {
            get { return "Simple Injector"; }
        }

        protected override ComposablePartCatalog GetComponentCatalog()
        {
            return new AssemblyCatalog(Assembly.GetExecutingAssembly());
        }
    }
}
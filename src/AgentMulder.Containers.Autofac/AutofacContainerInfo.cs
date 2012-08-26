using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Reflection;
using AgentMulder.ReSharper.Domain.Containers;

namespace AgentMulder.Containers.Autofac
{
    [Export(typeof(IContainerInfo))]
    public class AutofacContainerInfo : ContainerInfoBase
    {
        public override string ContainerDisplayName
        {
            get { return "Autofac"; }
        }

        protected override ComposablePartCatalog GetComponentCatalog()
        {
            return new AssemblyCatalog(Assembly.GetExecutingAssembly());
        }
    }
}
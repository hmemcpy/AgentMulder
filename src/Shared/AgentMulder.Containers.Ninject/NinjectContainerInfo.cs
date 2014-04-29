using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Reflection;
using AgentMulder.ReSharper.Domain.Containers;

namespace AgentMulder.Containers.Ninject
{
    [Export(typeof(IContainerInfo))]
    public class NinjectContainerInfo : ContainerInfoBase
    {
        public override IEnumerable<string> ContainerQualifiedNames
        {
            get
            {
                yield return "Ninject";
                yield return "Ninject.Modules";
            }
        }

        public override string ContainerDisplayName
        {
            get { return "Ninject"; }
        }

        protected override ComposablePartCatalog GetComponentCatalog()
        {
            return new AssemblyCatalog(Assembly.GetExecutingAssembly());
        }
    }
}
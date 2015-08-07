using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Reflection;
using AgentMulder.ReSharper.Domain.Containers;

namespace AgentMulder.Containers.Unity
{
    [Export(typeof(IContainerInfo))]
    public class UnityContainerInfo : ContainerInfoBase
    {
        public override string ContainerDisplayName
        {
            get { return "Unity"; }
        }

        public override IEnumerable<string> ContainerQualifiedNames
        {
            get { yield return "Microsoft.Practices.Unity"; }
        }

        protected override ComposablePartCatalog GetComponentCatalog()
        {
            return new AssemblyCatalog(Assembly.GetExecutingAssembly());
        }
    }
}
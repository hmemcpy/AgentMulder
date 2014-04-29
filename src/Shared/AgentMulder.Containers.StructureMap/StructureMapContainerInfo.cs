using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Reflection;
using AgentMulder.ReSharper.Domain.Containers;

namespace AgentMulder.Containers.StructureMap
{
    [Export(typeof(IContainerInfo))]
    public class StructureMapContainerInfo : ContainerInfoBase
    {
        public override IEnumerable<string> ContainerQualifiedNames
        {
            get
            {
                yield return "StructureMap";
                yield return "StructureMap.Configuration.DSL";
            }
        }

        public override string ContainerDisplayName
        {
            get { return "StructureMap"; }
        }

        protected override ComposablePartCatalog GetComponentCatalog()
        {
            return new AssemblyCatalog(Assembly.GetExecutingAssembly());
        }
    }
}
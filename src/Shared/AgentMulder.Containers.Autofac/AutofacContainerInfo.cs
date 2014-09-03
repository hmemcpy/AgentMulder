using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Reflection;
using AgentMulder.ReSharper.Domain.Containers;
using AgentMulder.ReSharper.Domain.Elements.Modules;
using AgentMulder.ReSharper.Domain.Elements.Modules.Impl;

namespace AgentMulder.Containers.Autofac
{
    [Export(typeof(IContainerInfo))]
    public class AutofacContainerInfo : ContainerInfoBase
    {
        public override string ContainerDisplayName
        {
            get { return "Autofac"; }
        }

        public override IEnumerable<string> ContainerQualifiedNames
        {
            get { yield return "Autofac"; }
        }

        public AutofacContainerInfo()
        {
            ModuleExtractor.AddExtractor(new CallingAssemblyExtractor("Autofac.Module", "ThisAssembly"));
        }

        protected override ComposablePartCatalog GetComponentCatalog()
        {
            return new AssemblyCatalog(Assembly.GetExecutingAssembly());
        }
    }
}
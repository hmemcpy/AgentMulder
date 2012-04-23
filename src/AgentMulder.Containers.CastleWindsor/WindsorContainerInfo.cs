using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using AgentMulder.ReSharper.Domain.Containers;
using AgentMulder.ReSharper.Domain.Search;

namespace AgentMulder.Containers.CastleWindsor
{
    [Export(typeof(IContainerInfo))]
    public class WindsorContainerInfo : IContainerInfo
    {
        public WindsorContainerInfo()
        {
            var catalog = new AssemblyCatalog(Assembly.GetCallingAssembly());
            var container = new CompositionContainer(catalog);
            container.ComposeParts(this);
        }

        public string ContainerDisplayName
        {
            get { return "Castle Windsor"; }
        }

        [ImportMany(typeof(IRegistrationPattern))]
        public IEnumerable<IRegistrationPattern> RegistrationPatterns { get; private set; }
    }
}
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using AgentMulder.ReSharper.Domain.Containers;
using AgentMulder.ReSharper.Domain.Patterns;
using AgentMulder.ReSharper.Domain.Registrations;

namespace AgentMulder.Containers.StructureMap
{
    [Export(typeof(IContainerInfo))]
    public class StructureMapContainerInfo : IContainerInfo
    {
        private readonly List<IRegistrationPattern> registrationPatterns; 

        public string ContainerDisplayName
        {
            get { return "StructureMap"; }
        }

        public IEnumerable<IRegistrationPattern> RegistrationPatterns
        {
            get { return registrationPatterns; }
        }

        [ImportMany]
        private IEnumerable<IRegistrationPatternsProvider> PatternsProviders { get; set; }

        public StructureMapContainerInfo()
        {
            var catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
            var container = new CompositionContainer(catalog);
            container.ComposeParts(this);

            registrationPatterns = PatternsProviders.SelectMany(provider => provider.GetRegistrationPatterns()).ToList();
        }

        public StructureMapContainerInfo(IEnumerable<IRegistrationPatternsProvider> patternsProviders)
        {
            PatternsProviders = patternsProviders;

            registrationPatterns = PatternsProviders.SelectMany(provider => provider.GetRegistrationPatterns()).ToList();
        }
    }
}
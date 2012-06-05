using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using AgentMulder.ReSharper.Domain.Containers;
using AgentMulder.ReSharper.Domain.Patterns;
using AgentMulder.ReSharper.Domain.Registrations;

namespace AgentMulder.Containers.Autofac
{
    [Export(typeof(IContainerInfo))]
    public class AutofacContainerInfo : IContainerInfo
    {
        private readonly List<IRegistrationPattern> registrationPatterns; 

        public string ContainerDisplayName
        {
            get { return "Autofac"; }
        }

        public IEnumerable<IRegistrationPattern> RegistrationPatterns
        {
            get { return registrationPatterns; }
        }

        [ImportMany]
        private IEnumerable<IRegistrationPatternsProvider> PatternsProviders { get; set; }

        public AutofacContainerInfo()
        {
            var catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
            var container = new CompositionContainer(catalog);
            container.ComposeParts(this);

            registrationPatterns = PatternsProviders.SelectMany(provider => provider.GetRegistrationPatterns()).ToList();
        }

        public AutofacContainerInfo(IEnumerable<IRegistrationPatternsProvider> patternsProviders)
        {
            PatternsProviders = patternsProviders;

            registrationPatterns = PatternsProviders.SelectMany(provider => provider.GetRegistrationPatterns()).ToList();
        }
    }
}
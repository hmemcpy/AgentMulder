using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using AgentMulder.Containers.Ninject.Patterns;
using AgentMulder.ReSharper.Domain.Containers;
using AgentMulder.ReSharper.Domain.Patterns;
using AgentMulder.ReSharper.Domain.Registrations;

namespace AgentMulder.Containers.Ninject
{
    [Export(typeof(IContainerInfo))]
    public class NinjectContainerInfo : IContainerInfo
    {
        private readonly List<IRegistrationPattern> registrationPatterns;

        public IEnumerable<IRegistrationPattern> RegistrationPatterns
        {
            get { return registrationPatterns; }
        }

        [ImportMany]
        private IEnumerable<IRegistrationPatternsProvider> PatternsProviders { get; set; }

        public string ContainerDisplayName
        {
            get { return "Ninject"; }
        }

        public NinjectContainerInfo()
        {
            var catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
            var container = new CompositionContainer(catalog);
            container.ComposeParts(this);

            var bindPatterns = PatternsProviders.SelectMany(provider => provider.GetRegistrationPatterns()).ToArray();
            registrationPatterns = new List<IRegistrationPattern>
            {
                new NinjectModulePattern(bindPatterns),
            };

            registrationPatterns = new List<IRegistrationPattern>(bindPatterns);
        }

        public NinjectContainerInfo(IEnumerable<IRegistrationPatternsProvider> patternsProviders)
        {
            PatternsProviders = patternsProviders;

            var bindPatterns = PatternsProviders.SelectMany(provider => provider.GetRegistrationPatterns()).ToArray();
            registrationPatterns = new List<IRegistrationPattern>
            {
                new NinjectModulePattern(bindPatterns),
            };

            registrationPatterns = new List<IRegistrationPattern>(bindPatterns);
        }
    }
}
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using AgentMulder.Containers.Ninject.Patterns;
using AgentMulder.Containers.Ninject.Patterns.Bind;
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

        public IEnumerable<string> ContainerQualifiedNames
        {
            get
            {
                yield return "Ninject";
                yield return "Ninject.Modules";
            }
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

            registrationPatterns = PatternsProviders.SelectMany(provider => provider.GetRegistrationPatterns()).ToList();
        }

        public NinjectContainerInfo(IEnumerable<IRegistrationPatternsProvider> patternsProviders)
        {
            PatternsProviders = patternsProviders;

            registrationPatterns = PatternsProviders.SelectMany(provider => provider.GetRegistrationPatterns()).ToList();
        }
    }
}
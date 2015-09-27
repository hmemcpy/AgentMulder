using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using AgentMulder.Containers.CastleWindsor.Patterns;
using AgentMulder.ReSharper.Domain.Containers;
using AgentMulder.ReSharper.Domain.Patterns;
using AgentMulder.ReSharper.Domain.Registrations;

namespace AgentMulder.Containers.CastleWindsor
{
    [Export(typeof(IContainerInfo))]
    public class WindsorContainerInfo : IContainerInfo
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
                yield return "Castle.Windsor";
                yield return "Castle.MicroKernel.Registration";
            }
        }

        [ImportMany]
        private IEnumerable<IRegistrationPatternsProvider> PatternsProviders { get; set; }

        public WindsorContainerInfo()
        {
            var catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
            var container = new CompositionContainer(catalog);
            container.ComposeParts(this);
            
            registrationPatterns = new List<IRegistrationPattern>
            {
                new WindsorContainerRegisterPattern(PatternsProviders.SelectMany(provider => provider.GetRegistrationPatterns()).ToArray())
            };
        }

        public WindsorContainerInfo(IEnumerable<IRegistrationPatternsProvider> patternsProviders)
        {
            PatternsProviders = patternsProviders;

            registrationPatterns = new List<IRegistrationPattern>
            {
                new WindsorContainerRegisterPattern(PatternsProviders.SelectMany(provider => provider.GetRegistrationPatterns()).ToArray())
            };
        }

        public string ContainerDisplayName
        {
            get { return "Castle Windsor"; }
        }
    }
}
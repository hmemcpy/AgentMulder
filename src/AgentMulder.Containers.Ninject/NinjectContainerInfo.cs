using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using AgentMulder.Containers.Ninject.Patterns;
using AgentMulder.ReSharper.Domain.Containers;
using AgentMulder.ReSharper.Domain.Registrations;
using AgentMulder.ReSharper.Domain.Search;

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

        public NinjectContainerInfo(IEnumerable<IRegistrationPatternsProvider> patternsProviders)
        {
            PatternsProviders = patternsProviders;

            registrationPatterns = new List<IRegistrationPattern>
            {
                new NinjectModulePattern(PatternsProviders.SelectMany(provider => provider.GetRegistrationPatterns()).ToArray())
            };
        }
    }
}
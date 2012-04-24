using System.Collections.Generic;
using System.ComponentModel.Composition;
using AgentMulder.Containers.CastleWindsor.Patterns;
using AgentMulder.Containers.CastleWindsor.Patterns.Component;
using AgentMulder.ReSharper.Domain.Containers;
using AgentMulder.ReSharper.Domain.Search;

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

        public WindsorContainerInfo()
        {
            registrationPatterns = new List<IRegistrationPattern> 
            {
                new ContainerRegisterPattern(
                    new ImplementedByGeneric(),
                    new ComponentForGeneric())
            };
        }

        public string ContainerDisplayName
        {
            get { return "Castle Windsor"; }
        }
    }
}
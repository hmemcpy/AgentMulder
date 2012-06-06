using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using AgentMulder.Containers.Unity.Patterns;
using AgentMulder.ReSharper.Domain.Containers;
using AgentMulder.ReSharper.Domain.Patterns;

namespace AgentMulder.Containers.Unity
{
    [Export(typeof(IContainerInfo))]
    public class UnityContainerInfo : IContainerInfo
    {
        private readonly List<IRegistrationPattern> registrationPatterns;

        public string ContainerDisplayName
        {
            get { return "Unity"; }
        }

        public IEnumerable<IRegistrationPattern> RegistrationPatterns
        {
            get { return registrationPatterns; }
        }

        
        public UnityContainerInfo()
        {
            registrationPatterns = new List<IRegistrationPattern>
            {
                new RegisterType(),
            };
        }
    }
}
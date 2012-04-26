using System.Collections.Generic;
using AgentMulder.ReSharper.Domain.Containers;
using AgentMulder.ReSharper.Domain.Search;

namespace AgentMulder.ReSharper.Tests
{
    public class TestContainerInfo : IContainerInfo
    {
        private readonly IRegistrationPattern[] patterns;

        public string ContainerDisplayName
        {
            get { return "Test"; }
        }

        public IEnumerable<IRegistrationPattern> RegistrationPatterns
        {
            get { return patterns; }
        }

        public TestContainerInfo(params IRegistrationPattern[] patterns)
        {
            this.patterns = patterns;
        }
    }
}
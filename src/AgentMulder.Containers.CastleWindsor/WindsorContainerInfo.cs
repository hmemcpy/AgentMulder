using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using AgentMulder.Containers.CastleWindsor.Patterns;
using AgentMulder.ReSharper.Domain.Containers;
using AgentMulder.ReSharper.Domain.Search;

namespace AgentMulder.Containers.CastleWindsor
{
    [Export(typeof(IContainerInfo))]
    public class WindsorContainerInfo : IContainerInfo
    {
        private readonly string[] assemblyNames = new[] { "Castle.Windsor" };

        private static readonly List<IComponentRegistrationPattern> patterns = new List<IComponentRegistrationPattern> 
        {
            new ServiceCompoisitePattern(),
            new FromTypesPattern()
        };

        public string ContainerDisplayName
        {
            get { return "Castle Windsor"; }
        }

        public IEnumerable<string> ContainerAssemblyNames
        {
            get { return assemblyNames; }
        }

        public IEnumerable<IComponentRegistrationPattern> RegistrationPatterns
        {
            get { return patterns; }
        }

        public bool HasContainerReference(IEnumerable<string> projectAssemblyReferences)
        {
            return projectAssemblyReferences.Any(reference => assemblyNames.Any(name => name == reference));
        }
    }
}
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using AgentMulder.Core;

namespace AgentMulder.Containers.CastleWindsor
{
    [Export(typeof(IContainerInfo))]
    public class WindsorContainerInfo : IContainerInfo
    {
        private readonly string[] assemblyNames = new[] { "Castle.Windsor.dll" };
        private readonly IRegistrationParser registrationParser;

        public WindsorContainerInfo()
        {
            registrationParser = new WindsorRegistrationParser();
        }

        public string Name
        {
            get { return "Castle Windsor"; }
        }

        public IEnumerable<string> AssemblyNames
        {
            get { return assemblyNames; }
        }

        public IRegistrationParser RegistrationParser
        {
            get { return registrationParser; }
        }

        public bool HasContainerReference(IEnumerable<string> projectAssemblyReferences)
        {
            return projectAssemblyReferences.Any(reference => assemblyNames.Any(name => name == reference));
        }
    }
}
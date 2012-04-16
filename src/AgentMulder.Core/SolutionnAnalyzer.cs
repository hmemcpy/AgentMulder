using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using AgentMulder.Core.TypeSystem;
using ICSharpCode.NRefactory.CSharp.TypeSystem;

namespace AgentMulder.Core
{
    public class SolutionnAnalyzer : ISolutionnAnalyzer
    {
        private readonly RegisteredTypesCollection registeredTypes = new RegisteredTypesCollection();

        public IEnumerable<Registration> RegisteredTypes
        {
            get { return registeredTypes; }
        }

        [ImportMany(typeof(IContainerInfo))]
        public IEnumerable<IContainerInfo> KnownContainers { get; private set; }

        public SolutionnAnalyzer(string rootDirectory)
        {
            var catalog = new DirectoryCatalog(rootDirectory, "*.dll");
            var container = new CompositionContainer(catalog);
            container.ComposeParts(this);
        }

        public void Analyze(ISolution solution)
        {
            foreach (var project in solution.Projects)
            {
                IProject current = project;
                var matchingContainers = from container in KnownContainers
                                         where container.HasContainerReference(
                                             current.ResolvedAssemblyReferences.Select(assembly => assembly.AssemblyName))
                                         select container;

                foreach (var matchingContainer in matchingContainers)
                {
                    IEnumerable<Registration> registrations = matchingContainer.RegistrationParser.Parse(project);
                    registeredTypes.AddRange(registrations);
                }
            }

        }
    }
}
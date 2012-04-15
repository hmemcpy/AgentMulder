using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using AgentMulder.Core.NRefactory;
using ICSharpCode.NRefactory.CSharp.TypeSystem;
using JetBrains.ProjectModel;

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

        public SolutionnAnalyzer()
        {
            var catalog = new DirectoryCatalog(".", "*.dll");
            var container = new CompositionContainer(catalog);
            container.ComposeParts(this);
        }

        public void Analyze(Solution solution)
        {
            foreach (var project in solution.Projects)
            {
                var context = new CSharpTypeResolveContext(project.Compilation.MainAssembly);
                var assemblyReferences =
                    project.ProjectContent.AssemblyReferences.Select(reference => reference.Resolve(context));

                var matchingContainers = from container in KnownContainers
                                         where container.HasContainerReference(
                                             assemblyReferences.Select(assembly => assembly.AssemblyName))
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
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using AgentMulder.Core.Patterns;
using AgentMulder.Core.ProjectModel;
using AgentMulder.ReSharper.Domain;
using AgentMulder.ReSharper.Domain.Containers;

namespace AgentMulder.Core
{
    public class SolutionAnalyzer
    {
        private readonly List<IComponentRegistration> registrations = new List<IComponentRegistration>();

        public IEnumerable<IComponentRegistration> Registrations
        {
            get { return registrations; }
        }

        [ImportMany(typeof(IContainerInfo))]
        private IEnumerable<IContainerInfo> KnownContainers { get; set; }

        public SolutionAnalyzer(params IContainerInfo[] knownContainers)
        {
            KnownContainers = knownContainers;
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
                    var projectAnalyzer = new ProjectAnalyzer(matchingContainer.RegistrationParser);
                    IEnumerable<IComponentRegistration> result = projectAnalyzer.GetRegisteredTypes(project);
                    registrations.AddRange(result);
                }
            }
        }
    }
}
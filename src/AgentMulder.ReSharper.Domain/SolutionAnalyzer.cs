using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using AgentMulder.ReSharper.Domain.Containers;
using AgentMulder.ReSharper.Domain.Registrations;
using AgentMulder.ReSharper.Domain.Search;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.Search;

namespace AgentMulder.ReSharper.Domain
{
    public class SolutionAnalyzer
    {
        private readonly SearchDomainFactory searchDomainFactory;
        private readonly List<IComponentRegistration> componentRegistrations = new List<IComponentRegistration>();

        public IEnumerable<IComponentRegistration> ComponentRegistrations
        {
            get { return componentRegistrations; }
        }

        [ImportMany(typeof(IContainerInfo))]
        private IEnumerable<IContainerInfo> KnownContainers { get; set; }

        public SolutionAnalyzer(SearchDomainFactory searchDomainFactory,  params IContainerInfo[] knownContainers)
        {
            this.searchDomainFactory = searchDomainFactory;
            KnownContainers = knownContainers;
        }

        public void Analyze(ISolution solution)
        {
            foreach (var project in solution.GetAllProjects())
            {
                IProject current = project;
                var matchingContainers = from container in KnownContainers
                                         where container.HasContainerReference(
                                             current.GetModuleReferences().Select(reference => reference.Name))
                                         select container;

                componentRegistrations.AddRange(
                    matchingContainers.SelectMany(containerInfo => GetProjectRegistrations(current, containerInfo)));
            }
        }

        private IEnumerable<IComponentRegistration> GetProjectRegistrations(IProject project, IContainerInfo containerInfo)
        {
            var patternSearcher = new PatternSearcher(project, searchDomainFactory);

            return containerInfo.RegistrationPatterns.SelectMany(pattern => pattern.CreateRegistrations(patternSearcher));
        }
    }
}
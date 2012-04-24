using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using AgentMulder.ReSharper.Domain.Containers;
using AgentMulder.ReSharper.Domain.Registrations;
using AgentMulder.ReSharper.Domain.Search;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.ReSharper.Domain
{
    public class SolutionAnalyzer
    {
        private readonly IPatternSearcher patternSearcher;

        [ImportMany(typeof(IContainerInfo))]
        private IEnumerable<IContainerInfo> KnownContainers { get; set; }

        public SolutionAnalyzer(IPatternSearcher patternSearcher,  params IContainerInfo[] knownContainers)
        {
            this.patternSearcher = patternSearcher;
            KnownContainers = knownContainers;
        }

        public IEnumerable<IComponentRegistration> Analyze(ISolution solution)
        {
            return KnownContainers.SelectMany(info => ScanRegistrations(solution, info));
        }

        private IEnumerable<IComponentRegistration> ScanRegistrations(ISolution solution, IContainerInfo containerInfo)
        {
            return from pattern in containerInfo.RegistrationPatterns
                   let results = patternSearcher.Search(pattern)
                   where results != null
                   from registration in pattern.GetComponentRegistrations(results.ToArray())
                   select registration;
        }
    }
}
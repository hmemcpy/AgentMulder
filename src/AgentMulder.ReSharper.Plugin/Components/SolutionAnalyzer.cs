using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using AgentMulder.ReSharper.Domain.Containers;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Caches;
using JetBrains.ReSharper.Psi.Search;
using JetBrains.Util;

namespace AgentMulder.ReSharper.Plugin.Components
{
    [SolutionComponent]
    public class SolutionAnalyzer
    {
        private readonly List<IContainerInfo> knownContainers = new List<IContainerInfo>();
        private readonly PatternSearcher patternSearcher;
        private readonly SearchDomainFactory searchDomainFactory;
        private readonly IWordIndex wordIndex;

        internal List<IContainerInfo> KnownContainers
        {
            get { return knownContainers; }
        }

        public SolutionAnalyzer(PatternSearcher patternSearcher, SearchDomainFactory searchDomainFactory, IWordIndex wordIndex)
        {
            this.patternSearcher = patternSearcher;
            this.searchDomainFactory = searchDomainFactory;
            this.wordIndex = wordIndex;

            LoadContainerInfos();
        }

        private void LoadContainerInfos()
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            Debug.Assert(path != null, "path != null");
            path = Path.Combine(path, "Containers");

            if (!Directory.Exists(path))
            {
                return;
            }

            var catalog = new DirectoryCatalog(path, "*.dll");
            var container = new CompositionContainer(catalog);
            IEnumerable<IContainerInfo> values = container.GetExportedValues<IContainerInfo>();
            knownContainers.AddRange(values);
        }

        public IEnumerable<RegistrationInfo> Analyze(IPsiSourceFile sourceFile)
        {
            IContainerInfo matchingContainer = GetMatchingContainer(sourceFile);
            if (matchingContainer == null)
            {
                return EmptyList<RegistrationInfo>.InstanceList;
            }

            ISearchDomain searchDomain = searchDomainFactory.CreateSearchDomain(sourceFile);

            return ScanRegistrations(matchingContainer, searchDomain);
        }

        private IContainerInfo GetMatchingContainer(IPsiSourceFile sourceFile)
        {
            return knownContainers.FirstOrDefault(knownContainer => 
                knownContainer.ContainerQualifiedNames.Any(s => wordIndex.CanContainWord(sourceFile, s)));
        }

        private IEnumerable<RegistrationInfo> ScanRegistrations(IContainerInfo containerInfo, ISearchDomain searchDomain)
        {
            return from pattern in containerInfo.RegistrationPatterns
                   let matchResults = patternSearcher.Search(pattern, searchDomain)
                   from matchResult in matchResults.Where(result => result.Matched)
                   from registration in pattern.GetComponentRegistrations(matchResult.MatchedElement)
                   select new RegistrationInfo(registration, containerInfo.ContainerDisplayName);
        }
    }
}
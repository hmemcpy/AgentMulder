using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using AgentMulder.ReSharper.Domain.Containers;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;

namespace AgentMulder.ReSharper.Plugin.Components
{
    [SolutionComponent]
    public class SolutionAnalyzer
    {
        private readonly PatternSearcher patternSearcher;
        private readonly ISolution solution;
        private readonly List<IContainerInfo> knownContainers = new List<IContainerInfo>();

        public void AddContainer(IContainerInfo containerInfo)
        {
            knownContainers.Add(containerInfo);
        }

        public SolutionAnalyzer(PatternSearcher patternSearcher, ISolution solution)
        {
            this.patternSearcher = patternSearcher;
            this.solution = solution;

            LoadContainerInfos();
        }

        private void LoadContainerInfos()
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
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

        
        public IEnumerable<RegistrationInfo> Analyze()
        {
            return knownContainers.SelectMany(info => ScanRegistrations(info));
        }

        public IEnumerable<RegistrationInfo> Analyze(IPsiSourceFile sourceFile)
        {
            return knownContainers.SelectMany(info => ScanRegistrations(info, sourceFile)).ToList();
        }

        private IEnumerable<RegistrationInfo> ScanRegistrations(IContainerInfo containerInfo, IPsiSourceFile sourceFile = null)
        {
            return from pattern in containerInfo.RegistrationPatterns
                   let matchResults = patternSearcher.Search(pattern, sourceFile)
                   from matchResult in matchResults.Where(result => result.Matched)
                   from registration in pattern.GetComponentRegistrations(matchResult.MatchedElement)
                   select new RegistrationInfo(registration, containerInfo.ContainerDisplayName);
        }
    }
}
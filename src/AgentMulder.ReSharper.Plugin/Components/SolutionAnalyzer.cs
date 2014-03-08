using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using AgentMulder.ReSharper.Domain.Containers;
using AgentMulder.ReSharper.Domain.Utils;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Search;
using JetBrains.Util;

namespace AgentMulder.ReSharper.Plugin.Components
{
    [SolutionComponent]
    public class SolutionAnalyzer
    {
        private readonly List<IContainerInfo> knownContainers = new List<IContainerInfo>();
        private readonly PatternSearcher patternSearcher;
        private readonly ISolution solution;
        private readonly SearchDomainFactory searchDomainFactory;

        internal List<IContainerInfo> KnownContainers
        {
            get { return knownContainers; }
        }

        public SolutionAnalyzer(PatternSearcher patternSearcher, ISolution solution, SearchDomainFactory searchDomainFactory)
        {
            this.patternSearcher = patternSearcher;
            this.solution = solution;
            this.searchDomainFactory = searchDomainFactory;

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

        
        public IEnumerable<RegistrationInfo> Analyze()
        {
            ISearchDomain searchDomain = searchDomainFactory.CreateSearchDomain(solution, false);
            
            return knownContainers.SelectMany(info => ScanRegistrations(info, searchDomain));
        }

        public IEnumerable<RegistrationInfo> Analyze(IPsiSourceFile sourceFile)
        {
            ICSharpFile cSharpFile = sourceFile.GetCSharpFile();
            if (cSharpFile == null)
            {
                return EmptyList<RegistrationInfo>.InstanceList;
            }

            // todo - optimization
            // determine all modules referenced from the source file
            // scan all type declarations in the file to see if any types' module is the container module
            
            var usingStatements = cSharpFile.Imports
                                            .Where(directive => !directive.ImportedSymbolName.QualifiedName.StartsWith("System"))
                                            .Select(directive => directive.ImportedSymbolName.QualifiedName).ToList();

            IContainerInfo matchingContainer = GetMatchingContainer(usingStatements);
            if (matchingContainer == null)
            {
                return EmptyList<RegistrationInfo>.InstanceList;
            }

            ISearchDomain searchDomain = searchDomainFactory.CreateSearchDomain(sourceFile);

            return ScanRegistrations(matchingContainer, searchDomain);
        }



        public IEnumerable<RegistrationInfo> Analyze(IEnumerable<IPsiSourceFile> sourceFiles)
        {
            ISearchDomain searchDomain = searchDomainFactory.CreateSearchDomain(sourceFiles);

            return knownContainers.SelectMany(info => ScanRegistrations(info, searchDomain));
        }

        private IContainerInfo GetMatchingContainer(IEnumerable<string> usingStatements)
        {
            return knownContainers.FirstOrDefault(knownContainer => 
                knownContainer.ContainerQualifiedNames.Any(usingStatements.Contains));
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
using System.Collections.Generic;
using System.Linq;
using AgentMulder.ReSharper.Domain.Patterns;
using JetBrains.Application;
using JetBrains.Application.Progress;
using JetBrains.DocumentManagers;
using JetBrains.ProjectModel;
using JetBrains.ProjectModel.Impl;
using JetBrains.ReSharper.Features.StructuralSearch.Finding;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.Caches;
using JetBrains.ReSharper.Psi.Search;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.StructuralSearch.Impl;
using JetBrains.Util;

namespace AgentMulder.ReSharper.Plugin.Components
{
    [SolutionComponent]
    public class PatternSearcher
    {
        private readonly ISolution solution;
        private readonly SearchDomainFactory searchDomainFactory;

        public PatternSearcher(ISolution solution)
        {
            this.solution = solution;
            searchDomainFactory = Shell.Instance.GetComponent<SearchDomainFactory>();
        }

        public IEnumerable<IStructuralMatchResult> Search(IStructuralPatternHolder pattern, IPsiSourceFile sourceFile = null)
        {
            var results = new List<IStructuralMatchResult>();
            var consumer = new FindResultConsumer<IStructuralMatchResult>(result =>
            {
                var findResultStructural = result as FindResultStructural;
                if (findResultStructural != null && findResultStructural.DocumentRange.IsValid())
                {
                    return findResultStructural.MatchResult;
                }

                return null;
            }, match =>
            {
                if (match != null)
                {
                    results.Add(match);
                }
                return FindExecution.Continue;
            });

            DoSearch(pattern.Matcher, consumer, sourceFile);

            return results;
        }

        private void DoSearch(IStructuralMatcher matcher, IFindResultConsumer<IStructuralMatchResult> consumer, IPsiSourceFile sourceFile)
        {
            
            ISearchDomain searchDomain = sourceFile == null
                                             ? searchDomainFactory.CreateSearchDomain(solution, false)
                                             : searchDomainFactory.CreateSearchDomain(sourceFile);
            
            var documentManager = solution.GetComponent<DocumentManager>();

            // todo add support for VB (eventually)
            var structuralSearcher = new StructuralSearcher(documentManager, CSharpLanguage.Instance, matcher);
            var searchDomainSearcher = new StructuralSearchDomainSearcher<IStructuralMatchResult>(
                NarrowSearchDomain(matcher.Words, matcher.GetExtendedWords(solution), searchDomain, searchDomainFactory),
                structuralSearcher, consumer, NullProgressIndicator.Instance, true);
            searchDomainSearcher.Run();
        }

        private ISearchDomain NarrowSearchDomain(IEnumerable<string> words, IEnumerable<string> extendedWords, ISearchDomain domain, SearchDomainFactory searchDomainFactory)
        {
            List<string> allWords = words.ToList();
            List<string> allExtendedWords = extendedWords.ToList();

            if (domain.IsEmpty || allWords.IsEmpty())
                return domain;
            IWordIndex wordIndex = solution.GetPsiServices().CacheManager.WordIndex;
            var jetHashSet1 = new JetHashSet<IPsiSourceFile>(wordIndex.GetFilesContainingWord(allWords.First()), null);
            foreach (string word in allWords.Skip(1))
                jetHashSet1.IntersectWith(wordIndex.GetFilesContainingWord(word));
            if (allExtendedWords.Any())
            {
                var jetHashSet2 = new JetHashSet<IPsiSourceFile>(null);
                using (JetHashSet<IPsiSourceFile>.ElementEnumerator enumerator = jetHashSet1.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        IPsiSourceFile file = enumerator.Current;
                        if (allExtendedWords.Any(word => wordIndex.CanContainWord(file, word)))
                            jetHashSet2.Add(file);
                    }
                }
                jetHashSet1 = jetHashSet2;
            }
            return domain.Intersect(searchDomainFactory.CreateSearchDomain(jetHashSet1));
        }
    }
}
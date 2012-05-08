using System.Collections.Generic;
using System.Linq;
using AgentMulder.ReSharper.Domain.Search;
using JetBrains.Application;
using JetBrains.Application.Progress;
using JetBrains.DocumentManagers;
using JetBrains.ProjectModel;
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
    [ShellComponent]
    public class PatternSearcher
    {
        private readonly SearchDomainFactory searchDomainFactory;

        public PatternSearcher(SearchDomainFactory searchDomainFactory)
        {
            this.searchDomainFactory = searchDomainFactory;
        }

        public IEnumerable<IStructuralMatchResult> Search(IRegistrationPattern patern)
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

            DoSearch(patern.Matcher, consumer);

            return results;
        }

        private void DoSearch(IStructuralMatcher matcher, FindResultConsumer<IStructuralMatchResult> consumer)
        {
            ISolution solution = Shell.Instance.GetComponent<ISolutionManager>().CurrentSolution;

            var searchDomain = searchDomainFactory.CreateSearchDomain(solution, false);
            var documentManager = solution.GetComponent<DocumentManager>();

            // todo add support for VB (eventually)
            var structuralSearcher = new StructuralSearcher(documentManager, CSharpLanguage.Instance, matcher);
            var searchDomainSearcher = new StructuralSearchDomainSearcher<IStructuralMatchResult>(
                NarrowSearchDomain(solution, matcher.Words, matcher.GetExtendedWords(solution), searchDomain),
                structuralSearcher, consumer, NullProgressIndicator.Instance, true);
            searchDomainSearcher.Run();
        }

        private ISearchDomain NarrowSearchDomain(ISolution solution, IEnumerable<string> words, IEnumerable<string> extendedWords, ISearchDomain domain)
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
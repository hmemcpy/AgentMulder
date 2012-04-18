using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Application.Progress;
using JetBrains.DocumentManagers;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Features.StructuralSearch.Finding;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Caches;
using JetBrains.ReSharper.Psi.Search;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.StructuralSearch.Impl;
using JetBrains.Util;

namespace AgentMulder.ReSharper.Domain.Search
{
    public class PatternSearcher : IPatternSearcher
    {
        private readonly IProject project;
        private readonly SearchDomainFactory domainFactory;

        public PatternSearcher(IProject project, SearchDomainFactory domainFactory)
        {
            this.project = project;
            this.domainFactory = domainFactory;
        }

        public IEnumerable<IStructuralMatchResult> Search(IComponentRegistrationPattern patern)
        {
            var solution = project.GetSolution();
            var searchDomain = domainFactory.CreateSearchDomain(solution, false);
            var documentManager = solution.GetComponent<DocumentManager>();
            
            IStructuralMatcher matcher = patern.Pattern.CreateMatcher();
            if (matcher == null)
                return null;

            var results = new List<IStructuralMatchResult>();
            var consumer = new FindResultConsumer<IStructuralMatchResult>(result =>
            {
                var findResultStructural = result as FindResultStructural;
                if (findResultStructural != null && findResultStructural.DocumentRange.IsValid())
                {
                    // todo get document range too, to navigate to registration!
                    return findResultStructural.MatchResult;
                }
                
                return (IStructuralMatchResult)null;
            }, match =>
            {
                if (match != null)
                {
                    results.Add(match);
                }
                return FindExecution.Continue;
            });

            var structuralSearcher = new StructuralSearcher(documentManager, patern.Pattern.Language, matcher);
            var searchDomainSearcher = new StructuralSearchDomainSearcher<IStructuralMatchResult>(
                NarrowSearchDomain(matcher.Words, matcher.GetExtendedWords(solution), searchDomain, solution), structuralSearcher, consumer, NullProgressIndicator.Instance, true);
            searchDomainSearcher.Run();

            return results;
        }
        
        private ISearchDomain NarrowSearchDomain(IEnumerable<string> words, IEnumerable<string> extendedWords, ISearchDomain domain, ISolution solution)
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
            return domain.Intersect(domainFactory.CreateSearchDomain(jetHashSet1));
        }
    }
}
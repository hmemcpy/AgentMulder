using System.Collections.Generic;
using System.Linq;
using AgentMulder.ReSharper.Domain.Registrations;
using AgentMulder.ReSharper.Domain.Search;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.Occurences;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.Containers.CastleWindsor.Patterns
{
    public class GenericComponentRegistrationPattern : IComponentRegistrationPattern
    {
        private readonly IStructuralSearchPattern pattern;

        public GenericComponentRegistrationPattern()
        {
            pattern = new CSharpStructuralSearchPattern("$component$.For<$service$>().ImplementedBy<$impl$>()",
                new ExpressionPlaceholder("component", "Castle.MicroKernel.Registration.Component"),
                new TypePlaceholder("service"),
                new TypePlaceholder("impl"));   
        }

        public IEnumerable<IComponentRegistration> CreateRegistrations(IPatternSearcher patternSearcher)
        {
            if (pattern.Check() != null)
            {
                yield break;
            }

            IEnumerable<IStructuralMatchResult> results = patternSearcher.Search(this);

            if (results == null)
            {
                yield break;
            }
        }

        public IStructuralMatcher CreateMatcher()
        {
            return pattern.CreateMatcher();
        }

        public IComponentRegistrationCreator GetComponentRegistrationCreator()
        {
            return new GenericComponentRegistrationCreator();
        }

        private sealed class GenericComponentRegistrationCreator : IComponentRegistrationCreator
        {
            public IEnumerable<IComponentRegistration> CreateRegistrations(ISolution solution, params IStructuralMatchResult[] matchResults)
            {
                return (from match in matchResults
                        let matchedType = match.GetMatchedType("impl") as IDeclaredType
                        where matchedType != null
                        select new ComponentRegistration(match.GetDocumentRange(), matchedType.GetTypeElement()));
            }
        }
    }
}
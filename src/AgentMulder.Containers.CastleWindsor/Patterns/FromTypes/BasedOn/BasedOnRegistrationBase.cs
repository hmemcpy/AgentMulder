using System.Collections.Generic;
using AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.WithService;
using AgentMulder.ReSharper.Domain.Registrations;
using AgentMulder.ReSharper.Domain.Search;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.BasedOn
{
    public abstract class BasedOnRegistrationBase : RegistrationBase
    {
        private readonly WithServiceRegistrationBase[] withServicePatterns;

        protected BasedOnRegistrationBase(IStructuralSearchPattern pattern, params WithServiceRegistrationBase[] withServicePatterns)
            : base(pattern)
        {
            this.withServicePatterns = withServicePatterns;
        }

        public override IEnumerable<IComponentRegistration> GetComponentRegistrations(params IStructuralMatchResult[] matchResults)
        {
            foreach (var withServicePattern in withServicePatterns)
            {
                IStructuralMatcher matcher = withServicePattern.CreateMatcher();

                foreach (var match in matchResults)
                {
                    IStructuralMatchResult matchResult = matcher.Match(match.MatchedElement);
                    if (matchResult.Matched)
                    {
                        foreach (var registration in withServicePattern.GetComponentRegistrations(matchResult))
                        {
                            yield return registration;
                        }
                    }
                }
            }
        }
    }
}
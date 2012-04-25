using System.Collections.Generic;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.Containers.CastleWindsor.Patterns.Component
{
    internal class ComponentForGeneric : ManualRegistrationBase
    {
        private readonly ManualRegistrationBase[] manualRegistrationPatterns;

        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$component$.For<$service$>()",
                                              new ExpressionPlaceholder("component", "Castle.MicroKernel.Registration.Component"),
                                              new TypePlaceholder("service"));

        public ComponentForGeneric(params ManualRegistrationBase[] manualRegistrationPatterns)
            : base(pattern, "service")
        {
            this.manualRegistrationPatterns = manualRegistrationPatterns;
        }

        public override IEnumerable<IComponentRegistration> GetComponentRegistrations(params IStructuralMatchResult[] matchResults)
        {
            foreach (var match in matchResults)
            {
                bool innerMatch = false;
                foreach (var innerPattern in manualRegistrationPatterns)
                {
                    IStructuralMatcher otherMatcher = innerPattern.CreateMatcher();
                    IStructuralMatchResult matchResult = otherMatcher.Match(match.MatchedElement);
                    if (matchResult.Matched)
                    {
                        foreach (var registration in innerPattern.GetComponentRegistrations(matchResult))
                        {
                            innerMatch = true;
                            yield return registration;
                        }
                    }
                }
                if (!innerMatch)
                {
                    foreach (var registration in base.GetComponentRegistrations(match))
                    {
                        yield return registration;
                    }
                }
            }
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using AgentMulder.ReSharper.Domain.Registrations;
#if SDK90
using JetBrains.ReSharper.Feature.Services.StructuralSearch;
#else
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
#endif

using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.ReSharper.Domain.Patterns
{
    public class CompositePattern : RegistrationPatternBase
    {
        private readonly IRegistrationPattern pattern;
        private readonly IBasedOnPattern[] basedOnPatterns;

        public CompositePattern(IRegistrationPattern pattern, params IBasedOnPattern[] basedOnPatterns)
            : base(pattern.Pattern)
        {
            this.pattern = pattern;
            this.basedOnPatterns = basedOnPatterns;
        }

        public override IEnumerable<IComponentRegistration> GetComponentRegistrations(ITreeNode registrationRootElement)
        {
            IStructuralMatchResult result = Match(registrationRootElement);

            if (result.Matched)
            {
                var innerRegistrations = (from p in basedOnPatterns
                                          from registration in p.GetBasedOnRegistrations(registrationRootElement)
                                          select registration).ToList();

                if (innerRegistrations.Any())
                {
                    var registrations = pattern.GetComponentRegistrations(registrationRootElement);

                    yield return new CompositeRegistration(registrationRootElement, innerRegistrations.Concat(registrations));
                }
            }
        }
    }
}
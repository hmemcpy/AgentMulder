using System.Collections.Generic;
using System.Linq;
using AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.WithService;
using AgentMulder.ReSharper.Domain.Registrations;
using AgentMulder.ReSharper.Domain.Search;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.BasedOn
{
    public abstract class BasedOnRegistrationBasePattern : RegistrationBasePattern
    {
        private readonly WithServiceRegistrationBasePattern[] withServicePatterns;

        protected BasedOnRegistrationBasePattern(IStructuralSearchPattern pattern, params WithServiceRegistrationBasePattern[] withServicePatterns)
            : base(pattern)
        {
            this.withServicePatterns = withServicePatterns;
        }

        public override IEnumerable<IComponentRegistration> GetComponentRegistrations(ITreeNode registrationRootElement)
        {
            return withServicePatterns.SelectMany(withServicePattern => withServicePattern.GetComponentRegistrations(registrationRootElement));
        }
    }
}
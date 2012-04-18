using System.Collections.Generic;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.ReSharper.Domain.Search
{
    public interface IComponentRegistrationPattern
    {
        IStructuralSearchPattern Pattern { get; }

        IEnumerable<IComponentRegistration> CreateRegistrations(IPatternSearcher patternSearcher);
    }
}
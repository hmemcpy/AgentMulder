using System.Collections.Generic;
using AgentMulder.ReSharper.Domain.Search;

namespace AgentMulder.ReSharper.Domain.Registrations
{
    public interface IRegistrationPatternsProvider
    {
        IEnumerable<RegistrationBasePattern> GetRegistrationPatterns();
    }
}
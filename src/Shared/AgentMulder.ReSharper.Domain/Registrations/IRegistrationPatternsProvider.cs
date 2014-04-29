using System.Collections.Generic;
using AgentMulder.ReSharper.Domain.Patterns;

namespace AgentMulder.ReSharper.Domain.Registrations
{
    public interface IRegistrationPatternsProvider
    {
        IEnumerable<IRegistrationPattern> GetRegistrationPatterns();
    }
}
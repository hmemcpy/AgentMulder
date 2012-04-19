using System.Collections.Generic;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.ReSharper.Domain.Search
{
    public interface IComponentRegistrationCreator
    {
        IEnumerable<IComponentRegistration> CreateRegistrations(params IStructuralMatchResult[] matchResults);
    }
}
using System.Collections.Generic;
using AgentMulder.ReSharper.Domain.Patterns;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.BasedOn
{
    public interface IBasedOnPattern : IRegistrationPattern
    {
        new IEnumerable<BasedOnRegistrationBase> GetComponentRegistrations(ITreeNode registrationRootElement);
    }
}
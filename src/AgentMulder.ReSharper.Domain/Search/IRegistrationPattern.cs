using System;
using System.Collections.Generic;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.ReSharper.Domain.Search
{
    public interface IRegistrationPattern
    {
        IStructuralMatcher CreateMatcher();
        IEnumerable<IComponentRegistration> GetComponentRegistrations(ITreeNode parentElement);
    }
}
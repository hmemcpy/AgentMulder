using System;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.ReSharper.Domain.Search
{
    public interface IRegistrationPattern
    {
        IStructuralMatcher CreateMatcher();
        IComponentRegistrationCreator CreateComponentRegistrationCreator();
    }
}
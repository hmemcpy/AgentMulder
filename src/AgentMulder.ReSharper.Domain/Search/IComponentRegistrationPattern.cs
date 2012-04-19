using System;
using System.Collections.Generic;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.ReSharper.Domain.Search
{
    public interface IComponentRegistrationPattern
    {
        IEnumerable<IComponentRegistration> CreateRegistrations(IPatternSearcher patternSearcher);
        IStructuralMatcher CreateMatcher();
        IComponentRegistrationCreator GetComponentRegistrationCreator();
    }

    public interface IComponentRegistrationCreator
    {
        IEnumerable<IComponentRegistration> CreateRegistrations(ISolution solution, params IStructuralMatchResult[] matchResults);
    }
}
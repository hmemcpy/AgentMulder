using System.Collections.Generic;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.ReSharper.Domain.Search
{
    public interface IPatternSearcher
    {
        IEnumerable<IStructuralMatchResult> Search(IComponentRegistrationPattern patern);
    }
}
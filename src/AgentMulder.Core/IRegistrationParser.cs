using System.Collections.Generic;
using AgentMulder.Core.ProjectModel;
using AgentMulder.ReSharper.Domain;

namespace AgentMulder.Core
{
    public interface IRegistrationParser
    {
        IEnumerable<IComponentRegistration> Parse(IProject project, SearchDomain);
    }
}
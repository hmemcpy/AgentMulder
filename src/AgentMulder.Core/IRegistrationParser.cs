using System.Collections.Generic;
using AgentMulder.Core.NRefactory;
using ICSharpCode.NRefactory.CSharp;

namespace AgentMulder.Core
{
    public interface IRegistrationParser
    {
        bool ParseInvocation(InvocationExpression invocationExpression);

        IEnumerable<Registration> Parse(CSharpProject project);
    }
}
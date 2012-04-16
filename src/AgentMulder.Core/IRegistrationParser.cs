using System.Collections.Generic;
using AgentMulder.Core.TypeSystem;
using ICSharpCode.NRefactory.CSharp;

namespace AgentMulder.Core
{
    public interface IRegistrationParser
    {
        bool ParseInvocation(InvocationExpression invocationExpression);

        IEnumerable<Registration> Parse(IProject project);
    }
}
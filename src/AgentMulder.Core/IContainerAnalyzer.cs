using System.Collections.Generic;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.ConsistencyCheck;

namespace AgentMulder.Core
{
    public interface IContainerAnalyzer
    {
        void Analyze(CSharpProject project);
        IEnumerable<string> RegisteredTypes { get; }

        bool IsContainerInvocation(InvocationExpression invocationExpression);
    }
}
using ICSharpCode.NRefactory.CSharp;

namespace AgentMulder.Core
{
    public class ContainerInvocationVisitor : DepthFirstAstVisitor
    {
        private readonly IContainerAnalyzer analyzer;

        public ContainerInvocationVisitor(IContainerAnalyzer analyzer)
        {
            this.analyzer = analyzer;
        }

        public override void VisitInvocationExpression(InvocationExpression invocationExpression)
        {
            if (!analyzer.IsContainerInvocation(invocationExpression))
            {
                base.VisitInvocationExpression(invocationExpression);   
            }
        }
    }
}
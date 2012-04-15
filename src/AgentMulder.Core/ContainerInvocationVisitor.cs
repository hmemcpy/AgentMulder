using ICSharpCode.NRefactory.CSharp;

namespace AgentMulder.Core
{
    public class ContainerInvocationVisitor : DepthFirstAstVisitor
    {
        private readonly IRegistrationParser parser;

        public ContainerInvocationVisitor(IRegistrationParser parser)
        {
            this.parser = parser;
        }

        public override void VisitInvocationExpression(InvocationExpression invocationExpression)
        {
            if (!parser.ParseInvocation(invocationExpression))
            {
                base.VisitInvocationExpression(invocationExpression);   
            }
        }
    }
}
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Matchers;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.ReSharper.Domain.Search
{
    public class InvocationExpressionMatcher : CSharpElementMatcher<IInvocationExpression>
    {
        private readonly CSharpSequenceMatcher<ICSharpArgument> myArgumentsMatcher;
        private readonly IElementMatcher myInvokedExpressionMatcher;

        public InvocationExpressionMatcher(IInvocationExpression expression, PatternMatcherBuilderParams @params)
        {
            myInvokedExpressionMatcher = AddMatcher(expression, e => e.InvokedExpression, @params);
            myArgumentsMatcher = AddMatcher(expression, e => e.Arguments, @params);
        }

        public override bool Match(ITreeNode element, IMatchingContext context)
        {
            if (base.Match(element, context))
            {
                return true;
            }
            var attribute = element as IAttribute;
            if ((attribute == null) || (attribute.Name == null))
            {
                return false;
            }
            return (myInvokedExpressionMatcher.Match(attribute.Name.NameIdentifier, context) &&
                    myArgumentsMatcher.Match(attribute.Arguments, context));
        }
    }
}
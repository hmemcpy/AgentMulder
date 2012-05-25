using System.Collections.Generic;
using AgentMulder.ReSharper.Domain.Patterns;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.Ninject.Patterns.Bind
{
    internal sealed class KernelBindNonGeneric : BindBasePattern
    {
        private static readonly IStructuralSearchPattern pattern = 
            new CSharpStructuralSearchPattern("$kernel$.Bind($service$)",
                new ExpressionPlaceholder("kernel", "global::Ninject.Syntax.BindingRoot", false),
                new ArgumentPlaceholder("service"));

        public KernelBindNonGeneric(params ComponentImplementationPatternBase[] toPatterns)
            : base(pattern, "service", toPatterns)
        {
        }

        protected override string GetXmlDocIdName(IMethod method)
        {
            return "M:Ninject.Syntax.BindingRoot.Bind(System.Type[])";
        }

        protected override IEnumerable<IComponentRegistration> DoCreateRegistrations(ITreeNode parentElement)
        {
            IStructuralMatchResult match = Match(parentElement);

            if (match.Matched)
            {
                var argument = match.GetMatchedElement(ElementName) as ICSharpArgument;
                if (argument == null)
                {
                    yield break;
                }

                // match typeof() expressions
                var typeOfExpression = argument.Value as ITypeofExpression;
                if (typeOfExpression != null)
                {
                    var typeElement = (IDeclaredType)typeOfExpression.ArgumentType;

                    yield return new ComponentRegistration(parentElement, typeElement.GetTypeElement());
                }
            }
        }
    }
}
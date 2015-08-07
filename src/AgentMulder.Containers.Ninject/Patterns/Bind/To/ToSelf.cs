using System.Collections.Generic;
using System.ComponentModel.Composition;
using AgentMulder.ReSharper.Domain.Patterns;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Feature.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Feature.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Feature.Services.StructuralSearch;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.Ninject.Patterns.Bind.To
{
    [Export(typeof(ComponentImplementationPatternBase))]
    public class ToSelf : ComponentImplementationPatternBase
    {
        private static readonly IStructuralSearchPattern pattern = 
            new CSharpStructuralSearchPattern("$bind$.ToSelf()",
                new ExpressionPlaceholder("bind", "global::Ninject.Syntax.IBindingSyntax", false));

        public ToSelf()
            : base(pattern, "bind")
        {
        }

        public override IEnumerable<IComponentRegistration> GetComponentRegistrations(ITreeNode registrationRootElement)
        {
            IStructuralMatchResult match = Match(registrationRootElement);

            if (match.Matched)
            {
                var invocationExpression = match.GetMatchedElement("bind") as IInvocationExpression;
                if (invocationExpression == null)
                {
                    yield break;
                }

                IDeclaredType declaredType = GetDeclaredType(invocationExpression);
                if (declaredType != null)
                {
                    ITypeElement typeElement = declaredType.GetTypeElement();
                    if (typeElement != null)
                    {
                        yield return new ComponentRegistration(registrationRootElement, typeElement);
                    }
                }
            }
        }

        private IDeclaredType GetDeclaredType(IInvocationExpression invocationExpression)
        {
            if (invocationExpression.TypeArguments.Count > 0)
            {
                return invocationExpression.TypeArguments[0].GetScalarType();
            }

            if (invocationExpression.ArgumentList.Arguments.Count > 0)
            {
                var typeOfExpression = invocationExpression.ArgumentList.Arguments[0].Value as ITypeofExpression;
                if (typeOfExpression == null)
                {
                    return null;
                }

                return typeOfExpression.ArgumentType.GetScalarType();
            }

            return null;
        }
    }
}
using System.Collections.Generic;
using AgentMulder.ReSharper.Domain.Patterns;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;
using System.Linq;

namespace AgentMulder.Containers.Ninject.Patterns.Bind.To
{
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

                var type = invocationExpression.TypeArguments.FirstOrDefault();
                if (type == null)
                {
                    yield break;
                }
                
                IDeclaredType declaredType = type.GetScalarType();
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
    }
}
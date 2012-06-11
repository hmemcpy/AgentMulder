using System.Collections.Generic;
using AgentMulder.ReSharper.Domain.Patterns;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Resolve;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.Autofac.Patterns
{
    internal sealed class RegisterLambdaExpression : RegistrationPatternBase
    {
        private static readonly IStructuralSearchPattern pattern = 
            new CSharpStructuralSearchPattern("$builder$.Register($args$ => $expression$)",
                new ExpressionPlaceholder("builder", "global::Autofac.ContainerBuilder", true),
                new ArgumentPlaceholder("args", -1, -1),
                new ExpressionPlaceholder("expression"));

        public RegisterLambdaExpression()
            : base(pattern)
        {
        }

        public override IEnumerable<IComponentRegistration> GetComponentRegistrations(ITreeNode registrationRootElement)
        {
            IStructuralMatchResult match = Match(registrationRootElement);

            if (match.Matched)
            {
                var expression = match.GetMatchedElement<IObjectCreationExpression>("expression");
                if (expression != null && expression.TypeReference != null)
                {
                    IResolveResult resolveResult = expression.TypeReference.Resolve().Result;
                    var @class = resolveResult.DeclaredElement as IClass;
                    if (@class != null)
                    {
                        yield return new ComponentRegistration(registrationRootElement, @class, @class);
                    }
                }
            }
        }
    }
}
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using AgentMulder.ReSharper.Domain.Patterns;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.Containers.SimpleInjector.Patterns
{
    [Export("ComponentRegistration", typeof(IRegistrationPattern))]
    public class LifestyleRegisterWithService : ReSharper.Domain.Patterns.RegisterWithService
    {
        // Lifestyle instance takes the Container as the parameter (or the last parameter in the non-generic version)
        // bug: for some reason, ReSharper cannot match this, so I omit the expression type...
        // bug: the search dialog CAN match it with the type, so I have no idea.
        // bug: the only thing that is evident, is that R# can't resolve the 'lifestyle' variable type
        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$lifestyle$.CreateRegistration($arguments$)",
                new ExpressionPlaceholder("lifestyle"),
                new ArgumentPlaceholder("arguments", -1, -1));

        public LifestyleRegisterWithService()
            : base(pattern)
        {
        }

        public override IEnumerable<IComponentRegistration> GetComponentRegistrations(JetBrains.ReSharper.Psi.Tree.ITreeNode registrationRootElement)
        {
            IStructuralMatchResult match = Match(registrationRootElement);

            if (match.Matched)
            {
                var invocationExpression = match.MatchedElement as IInvocationExpression;
                if (invocationExpression == null)
                {
                    yield break;
                }
            }
        }

        protected override IEnumerable<IComponentRegistration> FromArguments(IInvocationExpression invocationExpression)
        {
            List<ITypeofExpression> arguments = invocationExpression.ArgumentList.Arguments.Where(argument =>
            {
                var declaredType = argument.Value.Type() as IDeclaredType;
                return declaredType != null && declaredType.GetClrName().Equals(ClrTypeName);
            }).Select(argument => argument.Value as ITypeofExpression).ToList();

            var first = arguments.First().ArgumentType as IDeclaredType;
            var last = arguments.Last().ArgumentType as IDeclaredType;

            return CreateRegistration(invocationExpression, first, last);
        }
    }
}
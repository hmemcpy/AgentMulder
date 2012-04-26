using System.Collections.Generic;
using System.Linq;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.WithService
{
    internal class WithServiceBase : WithServiceRegistrationBase
    {
        private static readonly IStructuralSearchPattern pattern = 
            new CSharpStructuralSearchPattern("$basedOn$.WithServiceBase()",
                new ExpressionPlaceholder("basedOn", "Castle.MicroKernel.Registration.BasedOnDescriptor", true));

        public WithServiceBase()
            : base(pattern)
        {
        }

        public override IEnumerable<IComponentRegistration> GetComponentRegistrations(ITreeNode parentElement)
        {
            IStructuralMatchResult match = Match(parentElement);
            if (match.Matched)
            {
                var basedOnExpression = match.GetMatchedElement("basedOn") as IInvocationExpression;
                if (basedOnExpression == null)
                {
                    yield break;
                }

                var typeArgument = basedOnExpression.TypeArguments.First() as IDeclaredType;
                if (typeArgument != null)
                {
                    yield return new WithServiceRegistration(
                        match.MatchedElement.GetDocumentRange(), 
                        element => element.Equals(typeArgument.GetTypeElement()),
                        "Base Type");
                }
            }
        }
    }
}
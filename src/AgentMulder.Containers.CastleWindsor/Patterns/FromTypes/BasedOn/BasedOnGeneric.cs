using System.Collections.Generic;
using System.Linq;
using AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.WithService;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.BasedOn
{
    internal sealed class BasedOnGeneric : BasedOnRegistrationBase
    {
        private static readonly IStructuralSearchPattern pattern = 
            new CSharpStructuralSearchPattern("$anything$.BasedOn<$type$>()",
                new ExpressionPlaceholder("anything"),
                new TypePlaceholder("type"));

        public BasedOnGeneric(params WithServiceRegistrationBase[] withServicePatterns)
            : base(pattern, withServicePatterns)
        {
        }

        public override IEnumerable<IComponentRegistration> GetComponentRegistrations(params IStructuralMatchResult[] matchResults)
        {
            foreach (var withServiceRegistration in base.GetComponentRegistrations(matchResults).OfType<WithServiceRegistration>())
            {
                foreach (var match in matchResults)
                {
                    var matchedType = match.GetMatchedType("type") as IDeclaredType;
                    if (matchedType != null)
                    {
                        ITypeElement typeElement = matchedType.GetTypeElement(match.MatchedElement.GetPsiModule());
                        yield return new BasedOnRegistration(match.GetDocumentRange(), typeElement, withServiceRegistration);
                    }
                }   
            }
        }
    }
}
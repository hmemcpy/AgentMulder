using System.Collections.Generic;
using System.Linq;
using AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.WithService;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.BasedOn
{
    internal sealed class BasedOnGeneric : BasedOnRegistrationBasePattern
    {
        private static readonly IStructuralSearchPattern pattern = 
            new CSharpStructuralSearchPattern("$fromDescriptor$.BasedOn<$type$>()",
                new ExpressionPlaceholder("fromDescriptor", "Castle.MicroKernel.Registration.FromDescriptor", false),
                new TypePlaceholder("type"));

        public BasedOnGeneric(params WithServiceRegistrationBasePattern[] withServicePatterns)
            : base(pattern, withServicePatterns)
        {
        }

        public override IEnumerable<IComponentRegistration> GetComponentRegistrations(ITreeNode registrationRootElement)
        {
            IStructuralMatchResult match = Match(registrationRootElement);

            if (match.Matched)
            {
                var matchedType = match.GetMatchedType("type") as IDeclaredType;
                if (matchedType != null)
                {
                    ITypeElement typeElement = matchedType.GetTypeElement();
                    if (typeElement != null)
                    {
                        var withServiceRegistrations = base.GetComponentRegistrations(registrationRootElement).OfType<WithServiceRegistration>();

                        yield return new BasedOnRegistration(registrationRootElement, typeElement, withServiceRegistrations);
                    }
                }
            }
        }
    }
}
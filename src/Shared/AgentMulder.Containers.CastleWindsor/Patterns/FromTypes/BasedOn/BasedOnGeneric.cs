using System.Collections.Generic;
using AgentMulder.ReSharper.Domain.Patterns;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.BasedOn
{
    internal sealed class BasedOnGeneric : BasedOnPatternBase
    {
        private readonly IBasedOnRegistrationCreator registrationCreator;

        private static readonly IStructuralSearchPattern pattern = 
            new CSharpStructuralSearchPattern("$fromDescriptor$.BasedOn<$type$>()",
                new ExpressionPlaceholder("fromDescriptor", "Castle.MicroKernel.Registration.FromDescriptor", false),
                new TypePlaceholder("type"));

        public BasedOnGeneric(IBasedOnRegistrationCreator registrationCreator)
            : base(pattern)
        {
            this.registrationCreator = registrationCreator;
        }

        public override IEnumerable<IComponentRegistration> GetComponentRegistrations(ITreeNode registrationRootElement)
        {
            return GetBasedOnRegistrations(registrationRootElement);
        }

        public override IEnumerable<FilteredRegistrationBase> GetBasedOnRegistrations(ITreeNode registrationRootElement)
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
                        yield return registrationCreator.Create(registrationRootElement, typeElement);
                    }
                }
            }
        }
    }
}
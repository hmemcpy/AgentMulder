using System.Collections.Generic;
using System.Linq;
using AgentMulder.ReSharper.Domain.Patterns;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Caches;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.BasedOn
{
    internal sealed class Pick : BasedOnPatternBase
    {
        private readonly IBasedOnRegistrationCreator registrationCreator;

        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$fromDescriptor$.Pick()",
                new ExpressionPlaceholder("fromDescriptor", "Castle.MicroKernel.Registration.FromDescriptor", false));

        public Pick(IBasedOnRegistrationCreator registrationCreator)
            : base(pattern)
        {
            this.registrationCreator = registrationCreator;
        }

        public override IEnumerable<IComponentRegistration> GetComponentRegistrations(ITreeNode registrationRootElement)
        {
            return GetBasedOnRegistrations(registrationRootElement);
        }

        public override IEnumerable<BasedOnRegistrationBase> GetBasedOnRegistrations(ITreeNode registrationRootElement)
        {
            IStructuralMatchResult match = Match(registrationRootElement);

            if (match.Matched)
            {
                var declarationsCache = registrationRootElement.GetPsiServices().CacheManager.GetDeclarationsCache(DeclarationCacheLibraryScope.FULL, false);

                ITypeElement objectTypeElement = declarationsCache.GetTypeElementByCLRName("System.Object");
                if (objectTypeElement != null)
                {
                    yield return registrationCreator.Create(registrationRootElement, objectTypeElement);
                }
            }
        }
    }
}
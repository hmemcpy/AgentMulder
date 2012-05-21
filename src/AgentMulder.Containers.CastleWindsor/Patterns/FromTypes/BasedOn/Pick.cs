using System.Collections.Generic;
using System.Linq;
using AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.WithService;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.Metadata.Utils;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Caches;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.BasedOn
{
    internal class Pick : BasedOnRegistrationBasePattern
    {
        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$fromDescriptor$.Pick()",
                new ExpressionPlaceholder("fromDescriptor", "Castle.MicroKernel.Registration.FromDescriptor", false));

        public Pick(params WithServiceRegistrationBasePattern[] withServicePatterns)
            : base(pattern, withServicePatterns)
        {
        }

        public override IEnumerable<IComponentRegistration> GetComponentRegistrations(ITreeNode registrationRootElement)
        {
            IStructuralMatchResult match = Match(registrationRootElement);

            if (match.Matched)
            {
                var declarationsCache = registrationRootElement.GetPsiServices().CacheManager.GetDeclarationsCache(DeclarationCacheLibraryScope.FULL, false);

                ITypeElement objectTypeElement = declarationsCache.GetTypeElementByCLRName("System.Object");
                if (objectTypeElement != null)
                {
                    var withServiceRegistrations = base.GetComponentRegistrations(registrationRootElement).OfType<WithServiceRegistration>();

                    yield return new BasedOnRegistration(registrationRootElement, objectTypeElement, withServiceRegistrations);
                }
            }
        }
    }
}
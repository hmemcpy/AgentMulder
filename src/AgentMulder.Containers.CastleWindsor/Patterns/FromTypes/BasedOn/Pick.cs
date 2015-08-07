using System.Collections.Generic;
using AgentMulder.ReSharper.Domain.Patterns;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.Metadata.Reader.API;
using JetBrains.ReSharper.Psi;
#if SDK90
using JetBrains.ReSharper.Feature.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Feature.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Feature.Services.StructuralSearch;
#else
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
#endif
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.BasedOn
{
    public class Pick : BasedOnPatternBase
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

        public override IEnumerable<FilteredRegistrationBase> GetBasedOnRegistrations(ITreeNode registrationRootElement)
        {
            IStructuralMatchResult match = Match(registrationRootElement);

            if (match.Matched)
            {
                ITypeElement objectTypeElement = registrationRootElement.GetPsiModule()
                                                                        .GetPredefinedType(UniversalModuleReferenceContext.Instance)
                                                                        .Object.GetTypeElement();

                if (objectTypeElement != null)
                {
                    yield return registrationCreator.Create(registrationRootElement, objectTypeElement);
                }
            }
        }
    }
}
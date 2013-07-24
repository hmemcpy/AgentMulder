using System.Collections.Generic;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.BasedOn
{
    public partial class Pick
    {
        public override IEnumerable<FilteredRegistrationBase> GetBasedOnRegistrations(ITreeNode registrationRootElement)
        {
            IStructuralMatchResult match = Match(registrationRootElement);

            if (match.Matched)
            {
                ITypeElement objectTypeElement = TypeFactory.CreateTypeByCLRName("System.Object", registrationRootElement.GetPsiModule()).GetTypeElement();
                if (objectTypeElement != null)
                {
                    yield return registrationCreator.Create(registrationRootElement, objectTypeElement);
                }
            }
        }

    }
}
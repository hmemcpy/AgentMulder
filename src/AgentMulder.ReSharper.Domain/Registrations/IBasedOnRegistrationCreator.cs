using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.ReSharper.Domain.Registrations
{
    public interface IBasedOnRegistrationCreator
    {
        FilteredRegistrationBase Create(ITreeNode registrationRootElement, ITypeElement basedOnElement);
    }
}
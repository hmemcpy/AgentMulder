using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.ReSharper.Domain.Registrations
{
    public interface IComponentRegistration
    {
        ITreeNode RegistrationElement { get; }

        bool IsSatisfiedBy(ITypeElement typeElement);
    }
}
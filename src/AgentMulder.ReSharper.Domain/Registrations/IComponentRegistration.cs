using JetBrains.DocumentModel;
using JetBrains.ReSharper.Psi;

namespace AgentMulder.ReSharper.Domain.Registrations
{
    public interface IComponentRegistration
    {
        DocumentRange DocumentRange { get; }

        bool IsSatisfiedBy(ITypeElement typeElement);
    }
}
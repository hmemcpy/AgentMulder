using JetBrains.ReSharper.Psi;

namespace AgentMulder.ReSharper.Domain.Registrations
{
    public interface IComponentRegistration
    {
        bool IsSatisfiedBy(ITypeElement declaredType);
    }
}
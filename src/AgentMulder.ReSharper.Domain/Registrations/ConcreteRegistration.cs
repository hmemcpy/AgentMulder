using JetBrains.ReSharper.Psi;

namespace AgentMulder.ReSharper.Domain.Registrations
{
    public class ConcreteRegistration : IComponentRegistration
    {
        private readonly ITypeElement implementedByType;

        public ConcreteRegistration(ITypeElement implementedByType)
        {
            this.implementedByType = implementedByType;
        }

        public bool IsSatisfiedBy(ITypeElement declaredType)
        {
            return implementedByType.Equals(declaredType);
        }
    }
}
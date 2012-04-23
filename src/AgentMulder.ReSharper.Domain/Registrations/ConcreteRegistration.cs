using JetBrains.DocumentModel;
using JetBrains.ReSharper.Psi;

namespace AgentMulder.ReSharper.Domain.Registrations
{
    public class ConcreteRegistration : ComponentRegistrationBase
    {
        private readonly ITypeElement implementedType;
        private readonly string name;

        public ConcreteRegistration(DocumentRange documentRange, ITypeElement implementedType)
            : base(documentRange)
        {
            this.implementedType = implementedType;
            name = implementedType.GetClrName().FullName;
        }

        public override bool IsSatisfiedBy(ITypeElement typeElement)
        {
            return implementedType.Equals(typeElement);
        }

        public override string ToString()
        {
            return string.Format("Implemented by: {0}", name);
        }
    }
}
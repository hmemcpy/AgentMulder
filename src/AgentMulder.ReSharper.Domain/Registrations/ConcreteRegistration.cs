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
            name = GetDisplayName();
        }

        private string GetDisplayName()
        {
            IClrTypeName clrName = implementedType.GetClrName();

            string fullName = string.Format("{0}.{1}", clrName.GetNamespaceName(), clrName.ShortName);
            
            if (implementedType.HasTypeParameters())
            {
                fullName = string.Format("{0}<>", fullName);
            }

            return fullName;
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
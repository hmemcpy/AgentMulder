using JetBrains.DocumentModel;
using JetBrains.ReSharper.Psi;

namespace AgentMulder.ReSharper.Domain.Registrations
{
    public class ComponentRegistration : IComponentRegistration
    {
        private readonly DocumentRange documentRange;
        private readonly ITypeElement implementedType;

        public ComponentRegistration(DocumentRange documentRange, ITypeElement implementedType)
        {
            this.documentRange = documentRange;
            this.implementedType = implementedType;
        }

        public DocumentRange DocumentRange
        {
            get { return documentRange; }
        }

        public virtual bool IsSatisfiedBy(ITypeElement typeElement)
        {
            return implementedType.Equals(typeElement);
        }
    }
}
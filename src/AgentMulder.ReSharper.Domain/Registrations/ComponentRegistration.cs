using JetBrains.DocumentModel;
using JetBrains.ReSharper.Psi;

namespace AgentMulder.ReSharper.Domain.Registrations
{
    public class ComponentRegistration : IComponentRegistration
    {
        private readonly DocumentRange documentRange;
        private readonly ITypeElement implementedType;
        private string name;

        public ComponentRegistration(DocumentRange documentRange, ITypeElement implementedType)
        {
            this.documentRange = documentRange;
            this.implementedType = implementedType;
            name = implementedType.GetClrName().FullName;
        }

        public DocumentRange DocumentRange
        {
            get { return documentRange; }
        }

        public virtual bool IsSatisfiedBy(ITypeElement typeElement)
        {
            return implementedType.Equals(typeElement);
        }

        public override string ToString()
        {
            return string.Format("Implemented by: {0}", name);
        }
    }
}
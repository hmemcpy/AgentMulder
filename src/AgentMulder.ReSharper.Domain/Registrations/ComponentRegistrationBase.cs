using JetBrains.DocumentModel;
using JetBrains.ReSharper.Psi;

namespace AgentMulder.ReSharper.Domain.Registrations
{
    public abstract class ComponentRegistrationBase : IComponentRegistration
    {
        private readonly DocumentRange documentRange;

        protected ComponentRegistrationBase(DocumentRange documentRange)
        {
            this.documentRange = documentRange;
        }

        public DocumentRange DocumentRange
        {
            get { return documentRange; }
        }

        public abstract bool IsSatisfiedBy(ITypeElement typeElement);
    }
}
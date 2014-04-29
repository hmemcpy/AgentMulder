using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.ReSharper.Domain.Registrations
{
    public abstract class ComponentRegistrationBase : IComponentRegistration
    {
        private readonly ITreeNode registrationElement;

        protected ComponentRegistrationBase(ITreeNode registrationElement)
        {
            this.registrationElement = registrationElement;
        }  

        public ITreeNode RegistrationElement
        {
            get { return registrationElement; }
        }

        public abstract bool IsSatisfiedBy(ITypeElement typeElement);
    }
}
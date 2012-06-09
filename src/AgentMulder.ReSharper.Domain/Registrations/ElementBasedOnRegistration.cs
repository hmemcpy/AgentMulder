using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.ReSharper.Domain.Registrations
{
    public class ElementBasedOnRegistration : BasedOnRegistrationBase
    {
        public ElementBasedOnRegistration(ITreeNode registrationRootElement, ITypeElement basedOnElement)
            : base(registrationRootElement)
        {
            AddFilter(typeElement => typeElement.IsDescendantOf(basedOnElement));
        }
    }
}
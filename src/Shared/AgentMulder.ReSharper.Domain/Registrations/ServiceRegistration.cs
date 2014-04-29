using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.ReSharper.Domain.Registrations
{
    public class ServiceRegistration : FilteredRegistrationBase
    {
        public ServiceRegistration(ITreeNode registrationRootElement, ITypeElement serviceElement)
            : base(registrationRootElement)
        {
            AddFilter(typeElement => typeElement.IsDescendantOf(serviceElement));
        }
    }
}
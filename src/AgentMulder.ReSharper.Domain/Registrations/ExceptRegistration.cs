using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.ReSharper.Domain.Registrations
{
    internal sealed class ExceptRegistration : FilteredRegistrationBase
    {
        public ExceptRegistration(ITreeNode registrationRootElement, ITypeElement exceptElement)
            : base(registrationRootElement)
        {
            AddFilter(typeElement => !typeElement.Equals(exceptElement));
        }
    }
}
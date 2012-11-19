using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.Autofac.Registrations
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
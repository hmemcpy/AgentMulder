using System.ComponentModel.Composition;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.CastleWindsor.Registrations
{
    [Export("Classes", typeof(IBasedOnRegistrationCreator))]
    internal sealed class ClassesRegistrationCreator : IBasedOnRegistrationCreator
    {
        public FilteredRegistrationBase Create(ITreeNode registrationRootElement, ITypeElement basedOnElement)
        {
            return new ServiceRegistration(registrationRootElement, basedOnElement);
        }
    }
}
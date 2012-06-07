using System.ComponentModel.Composition;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.CastleWindsor.Registrations
{
    [Export("Classes", typeof(IBasedOnRegistrationCreator))]
    internal sealed class ClassesRegistrationCreator : IBasedOnRegistrationCreator
    {
        public BasedOnRegistrationBase Create(ITreeNode registrationRootElement, ITypeElement basedOnElement)
        {
            return new ClassesRegistration(registrationRootElement, basedOnElement);
        }

        private sealed class ClassesRegistration : BasedOnRegistrationBase
        {
            public ClassesRegistration(ITreeNode registrationRootElement, ITypeElement basedOnElement)
                : base(registrationRootElement)
            {
                AddFilter(typeElement => typeElement.IsDescendantOf(basedOnElement));
            }
        }
    }
}
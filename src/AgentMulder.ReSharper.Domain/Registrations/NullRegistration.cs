using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.ReSharper.Domain.Registrations
{
    public class NullRegistration : BasedOnRegistrationBase
    {
        public NullRegistration(ITreeNode registrationRootElement)
            : base(registrationRootElement)
        {
        }

        public override bool IsSatisfiedBy(ITypeElement typeElement)
        {
            return true;
        }
    }
}
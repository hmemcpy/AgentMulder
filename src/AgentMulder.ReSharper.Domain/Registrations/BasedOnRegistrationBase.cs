using System.Collections.Generic;
using System.Linq;
using JetBrains.DocumentModel;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.ReSharper.Domain.Registrations
{
    public abstract class BasedOnRegistrationBase : ComponentRegistrationBase
    {
        protected readonly IEnumerable<WithServiceRegistration> withServices;

        protected BasedOnRegistrationBase(ITreeNode registrationRootElement, IEnumerable<WithServiceRegistration> withServices)
            : base(registrationRootElement)
        {
            this.withServices = withServices.ToArray();
        }
    }
}
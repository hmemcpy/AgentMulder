using System.Collections.Generic;
using System.Linq;
using JetBrains.DocumentModel;

namespace AgentMulder.ReSharper.Domain.Registrations
{
    public abstract class BasedOnRegistrationBase : ComponentRegistrationBase
    {
        protected readonly IEnumerable<WithServiceRegistration> withServices;

        protected BasedOnRegistrationBase(DocumentRange documentRange, IEnumerable<WithServiceRegistration> withServices)
            : base(documentRange)
        {
            this.withServices = withServices.ToArray();
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.ReSharper.Domain.Registrations
{
    public abstract class BasedOnRegistrationBase : ComponentRegistrationBase
    {
        private readonly IEnumerable<WithServiceRegistration> withServices;

        protected BasedOnRegistrationBase(ITreeNode registrationRootElement, IEnumerable<WithServiceRegistration> withServices)
            : base(registrationRootElement)
        {
            this.withServices = withServices.ToArray();
        }

        public override bool IsSatisfiedBy(ITypeElement typeElement)
        {
            // todo not sure if this is correct
            if (typeElement is IInterface)
            {
                return false;
            }

            var @class = typeElement as IClass;
            if (@class != null && @class.IsAbstract)
            {
                return false;
            }

            return withServices.All(registration => registration.IsSatisfiedBy(typeElement));
        }

        public override string ToString()
        {
            return string.Join(", ", withServices.Select(registration => registration.ToString()));
        }
    }
}
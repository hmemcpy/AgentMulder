using System.Collections.Generic;
using System.Linq;
using JetBrains.ReSharper.Psi;

namespace AgentMulder.ReSharper.Domain.Registrations
{
    public class TypesBasedOnRegistration : ComponentRegistrationBase
    {
        private readonly FilteredRegistrationBase basedOn;
        private readonly IEnumerable<ITypeElement> types;

        public TypesBasedOnRegistration(IEnumerable<ITypeElement> types, FilteredRegistrationBase basedOn)
            : base(basedOn.RegistrationElement)
        {
            this.basedOn = basedOn;
            this.types = types.ToArray();
        }

        public override bool IsSatisfiedBy(ITypeElement typeElement)
        {
            if (!types.Contains(typeElement))
                return false;

            return basedOn.IsSatisfiedBy(typeElement);
        }

        public override string ToString()
        {
            return string.Format("From types: {0}, {1}",
                                 string.Join(", ", types.Select(registration => registration.ToString()), base.ToString()));
        }
    }
}
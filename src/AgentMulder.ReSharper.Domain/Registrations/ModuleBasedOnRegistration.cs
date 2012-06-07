using System.Linq;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using System.Collections.Generic;

namespace AgentMulder.ReSharper.Domain.Registrations
{
    public class ModuleBasedOnRegistration : ComponentRegistrationBase
    {
        private readonly IModule sourceModule;
        private readonly BasedOnRegistrationBase basedOn;

        public ModuleBasedOnRegistration(IModule sourceModule, BasedOnRegistrationBase basedOn)
            : base(basedOn.RegistrationElement)
        {
            this.sourceModule = sourceModule;
            this.basedOn = basedOn;
        }

        public override bool IsSatisfiedBy(ITypeElement typeElement)
        {
            IModule targetModule = typeElement.Module.ContainingProjectModule;
            if (targetModule == null)
            {
                return false;
            }

            if (sourceModule.Equals(targetModule))
            {
                if (basedOn != null)
                {
                    return basedOn.IsSatisfiedBy(typeElement);
                }
            }

            return false;
        }

        public override string ToString()
        {
            return string.Format("In module(s): {0}, {1}", sourceModule.Name, base.ToString());
        }
    }
}
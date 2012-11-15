using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;

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

            return sourceModule.Equals(targetModule) && basedOn.IsSatisfiedBy(typeElement);
        }

        public override string ToString()
        {
            return string.Format("In module(s): {0}, {1}", sourceModule.Name, basedOn);
        }
    }
}
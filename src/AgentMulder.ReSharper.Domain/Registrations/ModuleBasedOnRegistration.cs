using System.Linq;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using System.Collections.Generic;

namespace AgentMulder.ReSharper.Domain.Registrations
{
    public class ModuleBasedOnRegistration : ComponentRegistrationBase
    {
        private readonly IEnumerable<IModule> sourceModules;
        private readonly BasedOnRegistrationBase basedOn;

        public ModuleBasedOnRegistration(IModule sourceModule, BasedOnRegistrationBase basedOn)
            : this(new[] { sourceModule }, basedOn)
        {
        }

        public ModuleBasedOnRegistration(IEnumerable<IModule> sourceModules, BasedOnRegistrationBase basedOn)
            : base(basedOn.RegistrationElement)
        {
            this.sourceModules = sourceModules;
            this.basedOn = basedOn;
        }

        public override bool IsSatisfiedBy(ITypeElement typeElement)
        {
            IModule targetModule = typeElement.Module.ContainingProjectModule;
            if (targetModule == null)
            {
                return false;
            }

            return sourceModules.Any(targetModule.Equals) && basedOn.IsSatisfiedBy(typeElement);
        }

        public override string ToString()
        {
            return string.Format("In module(s): {0}, {1}", 
                string.Join(", ", sourceModules.Select(module => module.Name)), 
                base.ToString());
        }
    }
}
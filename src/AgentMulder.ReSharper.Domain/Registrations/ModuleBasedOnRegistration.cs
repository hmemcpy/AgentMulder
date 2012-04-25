using System.Linq;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;

namespace AgentMulder.ReSharper.Domain.Registrations
{
    public class ModuleBasedOnRegistration : BasedOnRegistration
    {
        private readonly IModule sourceModule;

        public ModuleBasedOnRegistration(IModule sourceModule, BasedOnRegistration basedOn)
            : base(basedOn.DocumentRange, basedOn.BasedOnElement)
        {
            this.sourceModule = sourceModule;
        }

        public override bool IsSatisfiedBy(ITypeElement typeElement)
        {
            IModule targetModule = typeElement.Module.ContainingProjectModule;
            if (targetModule == null)
                return false;

            return (targetModule.Equals(sourceModule) && base.IsSatisfiedBy(typeElement));
        }

        public override string ToString()
        {
            return string.Format("In module: {0}, {1}", sourceModule.Name, base.ToString());
        }
    }
}
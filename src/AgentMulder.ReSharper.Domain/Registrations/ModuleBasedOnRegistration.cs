using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.ReSharper.Domain.Registrations
{
    public class ModuleBasedOnRegistration : ComponentRegistrationBase
    {
        private readonly IModule sourceModule;

        public ModuleBasedOnRegistration(ITreeNode registrationElement, IModule sourceModule)
            : base(registrationElement)
        {
            this.sourceModule = sourceModule;
        }

        public override bool IsSatisfiedBy(ITypeElement typeElement)
        {
            IModule targetModule = typeElement.Module.ContainingProjectModule;
            if (targetModule == null)
            {
                return false;
            }

            return sourceModule.Equals(targetModule);
        }

        public override string ToString()
        {
            return string.Format("In module(s): {0}", sourceModule.Name);
        }
    }
}
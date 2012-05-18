using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.ReSharper.Plugin.Components
{
    public class RegistrationInfo
    {
        private readonly IComponentRegistration registration;
        private readonly string containerDisplayName;

        public IComponentRegistration Registration
        {
            get { return registration; }
        }

        public string ContainerDisplayName
        {
            get { return containerDisplayName; }
        }

        public RegistrationInfo(IComponentRegistration registration, string containerDisplayName)
        {
            this.registration = registration;
            this.containerDisplayName = containerDisplayName;
        }

        public IFile GetRegistrationFile()
        {
            return registration.RegistrationElement.GetContainingFile();
        }
    }
}
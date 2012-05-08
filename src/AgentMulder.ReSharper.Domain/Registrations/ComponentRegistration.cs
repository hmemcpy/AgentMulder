using JetBrains.DocumentModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.ReSharper.Domain.Registrations
{
    public class ComponentRegistration : ComponentRegistrationBase
    {
        private readonly ITypeElement serviceType;
        private readonly string name;
        
        private ITypeElement implementation;
        private string implementationName;

        public ITypeElement Implementation
        {
            get { return implementation; }
            set
            {
                implementation = value;
                implementationName = GetDisplayName(implementation);
            }
        }

        public ITypeElement ServiceType
        {
            get { return serviceType; }
        }

        public ComponentRegistration(ITreeNode registrationElement, ITypeElement serviceType)
            : base(registrationElement)
        {
            this.serviceType = serviceType;
            
            // for some reason, in tests this throws an exception
            // copying the name to display later in ToString()
            name = GetDisplayName(serviceType);
        }

        private static string GetDisplayName(ITypeElement element)
        {
            IClrTypeName clrName = element.GetClrName();

            string fullName = string.Format("{0}.{1}", clrName.GetNamespaceName(), clrName.ShortName);
            
            if (element.HasTypeParameters())
            {
                fullName = string.Format("{0}<>", fullName);
            }

            return fullName;
        }

        public override bool IsSatisfiedBy(ITypeElement typeElement)
        {
            if (implementation != null)
            {
                if ((implementation is IInterface) &&
                    implementation.HasTypeParameters())
                {
                    return IsAssignableFrom(typeElement, implementation);
                }

                return implementation.Equals(typeElement);
            }

            return serviceType.Equals(typeElement);
        }

        private bool IsAssignableFrom(ITypeElement typeElement, ITypeElement implementedBy)
        {
            return implementedBy.IsDescendantOf(implementedBy);
        }

        public override string ToString()
        {
            string displayName = implementationName ?? name;

            return string.Format("Implemented by: {0}", displayName);
        }
    }
}
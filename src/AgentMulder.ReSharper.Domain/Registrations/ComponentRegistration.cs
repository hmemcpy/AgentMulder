using JetBrains.DocumentModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.ReSharper.Domain.Registrations
{
    public class ComponentRegistration : ComponentRegistrationBase
    {
        private readonly ITypeElement serviceType;
        private ITypeElement implementation;
        
        public ITypeElement Implementation
        {
            get { return implementation; }
            set
            {
                implementation = value;
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
            // todo for now assume that interfaces are not supported
            if (typeElement is IInterface)
            {
                return false;
            }

            if (implementation != null)
            {
                if (implementation.IsGenericInterface())
                {
                    return typeElement.IsDescendantOf(implementation);
                }

                return implementation.Equals(typeElement);
            }

            return serviceType.Equals(typeElement);
        }

        public override string ToString()
        {
            string displayName = implementation != null ? implementation.GetClrName().FullName
                                                        : serviceType.GetClrName().FullName;

            return string.Format("Implemented by: {0}", displayName);
        }
    }
}
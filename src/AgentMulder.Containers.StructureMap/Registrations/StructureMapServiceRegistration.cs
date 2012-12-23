using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.StructureMap.Registrations
{
    internal class StructureMapServiceRegistration : StructureMapConvention
    {
        public StructureMapServiceRegistration(ITreeNode registrationRootElement, ITypeElement serviceType)
            : base(registrationRootElement)
        {
            AddFilter(typeElement => typeElement.IsDescendantOf(serviceType));
        }
    }
}
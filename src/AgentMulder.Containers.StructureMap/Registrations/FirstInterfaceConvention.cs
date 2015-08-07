using System.Linq;
using AgentMulder.ReSharper.Domain.Utils;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Util;

namespace AgentMulder.Containers.StructureMap.Registrations
{
    public class FirstInterfaceConvention : StructureMapConvention
    {
        public FirstInterfaceConvention(ITreeNode registrationRootElement)
            : base(registrationRootElement)
        {
            AddFilter(typeElement =>
            {
                if (!typeElement.IsConcrete())
                {
                    return false;
                }

                var firstInterface = typeElement.GetSuperTypes()
                                                .SelectNotNull(type => type.GetTypeElement())
                                                .OfType<IInterface>()
                                                .FirstOrDefault();

                if (firstInterface == null)
                {
                    return false;
                }

                return typeElement.IsDescendantOf(firstInterface);
            });
        }
    }
}
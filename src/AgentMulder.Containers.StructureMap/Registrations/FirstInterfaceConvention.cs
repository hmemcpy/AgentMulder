using System.Linq;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Util;
using AgentMulder.ReSharper.Domain.Utils;

namespace AgentMulder.Containers.StructureMap.Registrations
{
    public class FirstInterfaceConvention : StructureMapConventionBase
    {
        public FirstInterfaceConvention(ITreeNode registrationRootElement)
            : base(registrationRootElement)
        {
            AddFilter(element =>
            {
                if (!element.IsConcrete())
                {
                    return false;
                }

                var firstInterface = element.GetSuperTypes()
                                            .SelectNotNull(type => type.GetTypeElement())
                                            .OfType<IInterface>()
                                            .FirstOrDefault();

                if (firstInterface == null)
                {
                    return false;
                }

                return element.IsDescendantOf(firstInterface);
            });
        }
    }
}
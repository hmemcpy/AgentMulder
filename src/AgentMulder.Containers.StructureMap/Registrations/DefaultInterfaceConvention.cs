using System.Linq;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Util;

namespace AgentMulder.Containers.StructureMap.Registrations
{
    public class DefaultInterfaceConvention : StructureMapConvention
    {
        public DefaultInterfaceConvention(ITreeNode registrationRootElement)
            : base(registrationRootElement)
        {
            AddFilter(element =>
            {
                var interfaces = element.GetSuperTypes()
                                        .SelectNotNull(type => type.GetTypeElement())
                                        .OfType<IInterface>();

                return interfaces.Any(@interface => @interface.ShortName == "I" + element.ShortName);
            });
        }
    }
}
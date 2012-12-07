using System.Linq;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Util;

namespace AgentMulder.Containers.StructureMap.Registrations
{
    public class DefaultConvention : StructureMapConventionBase
    {
        public DefaultConvention(ITreeNode registrationRootElement)
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
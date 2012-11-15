using System.Linq;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Util;

namespace AgentMulder.Containers.StructureMap.Registrations
{
    internal class DefautConventionRegistration : BasedOnRegistrationBase
    {
        public DefautConventionRegistration(ITreeNode registrationRootElement)
            : base(registrationRootElement)
        {
            AddFilter(element =>
            {
                var interfaces = element.GetSuperTypes()
                                        .Select(type => type.GetTypeElement())
                                        .WhereNotNull()
                                        .OfType<IInterface>();

                return interfaces.Any(@interface => @interface.ShortName == "I" + element.ShortName);
            });
        }
    }
}
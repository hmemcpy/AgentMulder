using AgentMulder.ReSharper.Domain.Registrations;
using AgentMulder.ReSharper.Domain.Utils;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.Autofac.Registrations
{
    public class DefaultScanAssemblyRegistration : FilteredRegistrationBase
    {
        public DefaultScanAssemblyRegistration(ITreeNode registrationRootElement)
            : base(registrationRootElement)
        {
            AddFilter(typeElement =>
            {
                var @class = typeElement as IClass;
                if (@class == null)
                {
                    return false;
                }

                return !@class.IsAbstract &&
                       !@class.IsGenericTypeDefinition() &&
                       !@class.IsDelegate();
            });
        }
    }
}
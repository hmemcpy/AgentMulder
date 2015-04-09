using System.ComponentModel.Composition;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;
using AgentMulder.ReSharper.Domain.Utils;

namespace AgentMulder.Containers.CastleWindsor.Registrations
{
    [Export("Types", typeof(IBasedOnRegistrationCreator))]
    internal sealed class TypesRegistrationCreator : IBasedOnRegistrationCreator
    {
        public FilteredRegistrationBase Create(ITreeNode registrationRootElement, ITypeElement basedOnElement)
        {
            return new TypesRegistration(registrationRootElement, basedOnElement);
        }

        private sealed class TypesRegistration : ServiceRegistration
        {
            public TypesRegistration(ITreeNode registrationRootElement, ITypeElement basedOnElement)
                : base(registrationRootElement, basedOnElement)
            {
                AddFilter(typeElement =>
                {
                    var @class = typeElement as IClass;
                    if (@class == null || @class.IsAbstract || @class.IsStatic())
                    {
                        return false;
                    }

                    return true;
                });
            }
        }
    }
}
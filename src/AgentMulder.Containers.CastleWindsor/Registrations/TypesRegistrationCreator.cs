using System.ComponentModel.Composition;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.CastleWindsor.Registrations
{
    [Export("Types", typeof(IBasedOnRegistrationCreator))]
    internal sealed class TypesRegistrationCreator : IBasedOnRegistrationCreator
    {
        public BasedOnRegistrationBase Create(ITreeNode registrationRootElement, ITypeElement basedOnElement)
        {
            return new TypesRegistration(registrationRootElement, basedOnElement);
        }

        private sealed class TypesRegistration : BasedOnRegistrationBase
        {
            public TypesRegistration(ITreeNode registrationRootElement, ITypeElement basedOnElement)
                : base(registrationRootElement)
            {
                AddFilter(typeElement =>
                {
                    var @class = typeElement as IClass;
                    if (@class != null && @class.IsAbstract)
                    {
                        return false;
                    }

                    return true;
                });
                AddFilter(typeElement => typeElement.IsDescendantOf(basedOnElement));
            }
        }
    }
}
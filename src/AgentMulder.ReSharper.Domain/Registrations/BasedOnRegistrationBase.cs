using System;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.ReSharper.Domain.Registrations
{
    public abstract class BasedOnRegistrationBase : ComponentRegistrationBase
    {
        private Predicate<ITypeElement> defaultFilter = typeElement =>
        {
            if (typeElement is IInterface)
            {
                return false;
            }

            var @class = typeElement as IClass;
            if (@class != null && @class.IsAbstract)
            {
                return false;
            }

            return true;
        };

        public IModule Module { get; set; }

        protected BasedOnRegistrationBase(ITreeNode registrationRootElement)
            : base(registrationRootElement)
        {
        }

        public void AddFilter(Predicate<ITypeElement> condition)
        {
            defaultFilter += condition;
        }

        public override bool IsSatisfiedBy(ITypeElement typeElement)
        {
            if (defaultFilter(typeElement))
            {
                return true;
            }

            return false;
        }
    }
}
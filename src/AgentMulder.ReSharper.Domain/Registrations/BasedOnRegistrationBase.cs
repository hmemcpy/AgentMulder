using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.ReSharper.Domain.Registrations
{
    public abstract class BasedOnRegistrationBase : ComponentRegistrationBase
    {
        private readonly List<Predicate<ITypeElement>> filters = new List<Predicate<ITypeElement>>(); 
        
        protected BasedOnRegistrationBase(ITreeNode registrationRootElement)
            : base(registrationRootElement)
        {
            filters.Add(typeElement => !(typeElement is IInterface));
        }

        protected void AddFilter(Predicate<ITypeElement> condition)
        {
            filters.Add(condition);
        }

        public override bool IsSatisfiedBy(ITypeElement typeElement)
        {
            return filters.All(predicate => predicate(typeElement));
        }
    }
}
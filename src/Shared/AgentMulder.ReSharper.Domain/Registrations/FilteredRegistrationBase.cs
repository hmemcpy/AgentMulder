using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.ReSharper.Domain.Registrations
{
    public abstract class FilteredRegistrationBase : ComponentRegistrationBase
    {
        private readonly List<Predicate<ITypeElement>> filters = new List<Predicate<ITypeElement>>(); 
        
        protected FilteredRegistrationBase(ITreeNode registrationRootElement)
            : base(registrationRootElement)
        {
            filters.Add(typeElement => !(typeElement is IInterface));
            filters.Add(typeElement => !(typeElement is IEnum));
            filters.Add(typeElement => !(typeElement is IStruct));
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
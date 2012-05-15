using System;
using System.Collections.Generic;
using AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.BasedOn;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.Containers.CastleWindsor.Patterns.FromTypes
{
    internal abstract class ClassesFromBase : FromTypesBasePattern
    {
        protected ClassesFromBase(IStructuralSearchPattern pattern, IEnumerable<BasedOnRegistrationBasePattern> basedOnPatterns)
            : base(pattern, basedOnPatterns)
        {
        }

        protected override Predicate<ITypeElement> Filter
        {
            get
            {
                return typeElement =>
                {
                    var @class = typeElement as IClass;
                    if (@class != null)
                    {
                        return !@class.IsAbstract;
                    }

                    return false;
                };
            }
        }
    }
}
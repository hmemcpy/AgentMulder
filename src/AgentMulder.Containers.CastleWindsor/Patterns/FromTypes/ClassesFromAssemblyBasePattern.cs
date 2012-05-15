using System;
using AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.BasedOn;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.Containers.CastleWindsor.Patterns.FromTypes
{
    internal abstract class ClassesFromAssemblyBasePattern : FromAssemblyBasePattern
    {
        protected ClassesFromAssemblyBasePattern(IStructuralSearchPattern pattern, params BasedOnRegistrationBasePattern[] basedOnPatterns)
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
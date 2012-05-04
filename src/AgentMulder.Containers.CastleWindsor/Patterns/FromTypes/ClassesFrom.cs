using System.Collections.Generic;
using System.Linq;
using AgentMulder.Containers.CastleWindsor.Patterns.FromTypes;
using AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.BasedOn;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.CastleWindsor.Patterns.FromTypes
{
    internal sealed class ClassesFrom : FromTypesBasePattern
    {
        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$classes$.From($services$)",
                new ExpressionPlaceholder("classes", "Castle.MicroKernel.Registration.Classes"),
                new ArgumentPlaceholder("services", -1, -1)); // matches any number of arguments

        public ClassesFrom(params BasedOnRegistrationBasePattern[] basedOnPatterns)
            : base(pattern, basedOnPatterns)
        {
        }

        protected override IComponentRegistration CreateRegistration(IStructuralMatchResult match, BasedOnRegistration basedOnRegistration, IEnumerable<ITypeElement> typeElements)
        {
            // get all non-abstract classes (same as AllTypes)
            typeElements = typeElements.OfType<IClass>().Where(c => !c.IsAbstract);

            return base.CreateRegistration(match, basedOnRegistration, typeElements);
        }
    }
}
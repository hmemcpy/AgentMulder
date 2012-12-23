using System.Collections.Generic;
using AgentMulder.ReSharper.Domain.Patterns;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.Autofac.Patterns.FromAssemblies.BasedOn
{
    internal sealed class AssignableToGeneric : MultipleMatchBasedOnPatternBase
    {
        private static readonly IStructuralSearchPattern pattern = 
            new CSharpStructuralSearchPattern("$builder$.AssignableTo<$service$>()",
                new ExpressionPlaceholder("builder", "global::Autofac.Builder.IRegistrationBuilder<,,>", false),
                new TypePlaceholder("service"));

        public AssignableToGeneric()
            : base(pattern)
        {
        }

        protected override IEnumerable<FilteredRegistrationBase> DoCreateRegistrations(ITreeNode registrationRootElement, IStructuralMatchResult match)
        {
            var matchedType = match.GetMatchedType("service") as IDeclaredType;
            if (matchedType != null)
            {
                ITypeElement typeElement = matchedType.GetTypeElement();
                if (typeElement != null)
                {
                    // todo possible bug here. Investigate wheather As and AssignableTo differ somehow in the result.
                    yield return new ServiceRegistration(registrationRootElement, typeElement);
                }
            }
        }
    }
}
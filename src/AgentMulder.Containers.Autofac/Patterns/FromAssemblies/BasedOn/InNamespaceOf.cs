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
    internal sealed class InNamespaceOf : MultipleMatchBasedOnPatternBase
    {
        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$builder$.InNamespaceOf<$type$>()",
                new ExpressionPlaceholder("builder", "global::Autofac.Builder.IRegistrationBuilder<,,>", false),
                new TypePlaceholder("type"));

        public InNamespaceOf()
            : base(pattern)
        {
        }

        protected override IEnumerable<FilteredRegistrationBase> DoCreateRegistrations(ITreeNode registrationRootElement, IStructuralMatchResult match)
        {
            var declaredType = match.GetMatchedType("type") as IDeclaredType;
            if (declaredType != null)
            {
                ITypeElement typeElement = declaredType.GetTypeElement();
                if (typeElement != null)
                {
                    yield return new InNamespaceRegistration(registrationRootElement, typeElement.GetContainingNamespace(), true);
                }
            }
        }
    }
}
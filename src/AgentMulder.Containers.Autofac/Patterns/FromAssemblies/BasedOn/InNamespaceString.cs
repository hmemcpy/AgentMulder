using System.Collections.Generic;
using AgentMulder.ReSharper.Domain.Patterns;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.Autofac.Patterns.FromAssemblies.BasedOn
{
    internal sealed class InNamespaceString : MultipleMatchBasedOnPatternBase
    {
        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$builder$.InNamespace($argument$)",
                new ExpressionPlaceholder("builder", "global::Autofac.Builder.IRegistrationBuilder<,,>"),
                new ArgumentPlaceholder("argument"));

        public InNamespaceString()
            : base(pattern)
        {
        }

        protected override IEnumerable<FilteredRegistrationBase> DoCreateRegistrations(ITreeNode registrationRootElement, IStructuralMatchResult match)
        {
            var argument = match.GetMatchedElement("argument") as ICSharpArgument;
            if (argument != null)
            {
                INamespace @namespace = ReSharper.Domain.Utils.PsiExtensions.GetNamespaceDeclaration(argument.Value);
                if (@namespace != null)
                {
                    yield return new InNamespaceRegistration(registrationRootElement, @namespace, true);
                }
            }
        }
    }
}
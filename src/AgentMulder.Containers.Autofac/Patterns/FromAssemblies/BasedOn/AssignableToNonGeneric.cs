using System;
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
    internal sealed class AssignableToNonGeneric : MultipleMatchBasedOnPatternBase
    {
        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$builder$.AssignableTo($argument$)",
                new ExpressionPlaceholder("builder",
                    "global::Autofac.Builder.IRegistrationBuilder<object,global::Autofac.Features.Scanning.ScanningActivatorData,global::Autofac.Builder.DynamicRegistrationStyle>", false),
                new ArgumentPlaceholder("argument"));

        public AssignableToNonGeneric()
            : base(pattern)
        {
        }

        protected override IEnumerable<BasedOnRegistrationBase> DoCreateRegistrations(ITreeNode registrationRootElement, IStructuralMatchResult match)
        {
            var argument = match.GetMatchedElement("argument") as ICSharpArgument;
            if (argument == null)
            {
                yield break;
            }

            var typeofExpression = argument.Value as ITypeofExpression;
            if (typeofExpression != null)
            {
                var declaredType = typeofExpression.ArgumentType as IDeclaredType;
                if (declaredType != null)
                {
                    ITypeElement typeElement = declaredType.GetTypeElement();
                    if (typeElement != null)
                    {
                        // todo possible bug: same as in the generic variant. Currently works the same as As<T>.
                        yield return new ElementBasedOnRegistration(registrationRootElement, typeElement);
                    }
                }
            }
        }
    }
}
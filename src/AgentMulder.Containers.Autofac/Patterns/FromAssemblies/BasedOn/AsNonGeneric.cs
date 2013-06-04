using System.Collections.Generic;
using System.Linq;
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
    internal sealed class AsNonGeneric : MultipleMatchBasedOnPatternBase
    {
        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$builder$.As($arguments$)",
#if SDK70
                new ExpressionPlaceholder("builder", "global::Autofac.Builder.IRegistrationBuilder<,,>", false),
#else
                new ExpressionPlaceholder("builder",
                    "global::Autofac.Builder.IRegistrationBuilder<object,global::Autofac.Features.Scanning.ScanningActivatorData,global::Autofac.Builder.DynamicRegistrationStyle>", false),
#endif
                new ArgumentPlaceholder("arguments", -1, -1));

        public AsNonGeneric()
            : base(pattern)
        {
        }

        protected override IEnumerable<FilteredRegistrationBase> DoCreateRegistrations(ITreeNode registrationRootElement, IStructuralMatchResult match)
        {
            var arguments = match.GetMatchedElementList("arguments").Cast<ICSharpArgument>();

            foreach (var argument in arguments)
            {
                // match typeof() expressions
                var typeOfExpression = argument.Value as ITypeofExpression;
                if (typeOfExpression != null)
                {
                    var argumentType = typeOfExpression.ArgumentType as IDeclaredType;
                    if (argumentType != null)
                    {
                        var typeElement = argumentType.GetTypeElement();
                        if (typeElement == null) // can happen if the typeof() expression is empty
                        {
                            yield break;
                        }

                        yield return new ServiceRegistration(registrationRootElement, typeElement);
                    }
                }
            }
        }
    }
}
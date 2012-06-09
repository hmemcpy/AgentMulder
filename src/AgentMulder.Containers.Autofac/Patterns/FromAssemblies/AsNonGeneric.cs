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

namespace AgentMulder.Containers.Autofac.Patterns.FromAssemblies
{
    internal sealed class AsNonGeneric : BasedOnPatternBase
    {
        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$builder$.As($arguments$)",
                new ExpressionPlaceholder("builder",
                    "global::Autofac.Builder.IRegistrationBuilder<object,global::Autofac.Features.Scanning.ScanningActivatorData,global::Autofac.Builder.DynamicRegistrationStyle>", false),
                new ArgumentPlaceholder("arguments", -1, -1));

        public AsNonGeneric()
            : base(pattern)
        {
        }

        public override IEnumerable<IComponentRegistration> GetComponentRegistrations(ITreeNode registrationRootElement)
        {
            return GetBasedOnRegistrations(registrationRootElement);
        }

        public override IEnumerable<BasedOnRegistrationBase> GetBasedOnRegistrations(ITreeNode registrationRootElement)
        {
            IStructuralMatchResult match = Match(registrationRootElement);

            if (match.Matched)
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

                            yield return new ElementBasedOnRegistration(registrationRootElement, typeElement);

                        }
                    }   
                }
            }
        }
    }
}
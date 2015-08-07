using System.Collections.Generic;
using System.ComponentModel.Composition;
using AgentMulder.ReSharper.Domain.Patterns;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Feature.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Feature.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Feature.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.StructureMap.Patterns.For.Add
{
    [Export("NonGeneric", typeof(ComponentImplementationPatternBase))]
    internal sealed class AddNonGeneric : ComponentImplementationPatternBase
    {
        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$expr$.Add($service$)",
                new ExpressionPlaceholder("expr", "global::StructureMap.Configuration.DSL.Expressions.GenericFamilyExpression", true),
                new ArgumentPlaceholder("service"));

        public AddNonGeneric()
            : base(pattern, "service")
        {
        }

        public override IEnumerable<IComponentRegistration> GetComponentRegistrations(ITreeNode registrationRootElement)
        {
            IStructuralMatchResult match = Match(registrationRootElement);

            if (match.Matched)
            {
                var argument = match.GetMatchedElement(ElementName) as ICSharpArgument;
                if (argument == null)
                {
                    yield break;
                }

                // match typeof() expressions
                var typeOfExpression = argument.Value as ITypeofExpression;
                if (typeOfExpression != null)
                {
                    var typeElement = ((IDeclaredType)typeOfExpression.ArgumentType).GetTypeElement();
                    if (typeElement == null) // can happen if the typeof() expression is empty
                    {
                        yield break;
                    }

                    yield return new ComponentRegistration(registrationRootElement, typeElement);
                }
            }
        }
    }
}
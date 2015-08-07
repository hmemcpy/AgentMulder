using System.Collections.Generic;
using AgentMulder.ReSharper.Domain.Patterns;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Feature.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Feature.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Feature.Services.StructuralSearch;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.CastleWindsor.Patterns.Component.ComponentFor
{
    internal sealed class ComponentForNonGeneric : ComponentForBasePattern
    {
        // there seems to be an issue in ReSharper matching open generic types (such as typeof(IEnumerable<>)).
        // changing to match the argument instead, and extract the typeof expression manually

        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$component$.For($service$)",
                                              new ExpressionPlaceholder("component", "Castle.MicroKernel.Registration.Component"),
                                              new ArgumentPlaceholder("service"));

        public ComponentForNonGeneric(params ComponentImplementationPatternBase[] implementedByPatterns)
            : base(pattern, "service", implementedByPatterns)
        {

        }

        protected override IEnumerable<IComponentRegistration> DoCreateRegistrations(ITreeNode parentElement)
        {
            IStructuralMatchResult match = Match(parentElement);

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
                    var typeElement = (IDeclaredType)typeOfExpression.ArgumentType;

                    yield return new ComponentRegistration(parentElement, typeElement.GetTypeElement());
                }
            }
        }
    }
}
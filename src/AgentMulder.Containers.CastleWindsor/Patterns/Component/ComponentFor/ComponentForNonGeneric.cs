using System.Collections.Generic;
using System.Linq;
using AgentMulder.Containers.CastleWindsor.Patterns.Component.ImplementedBy;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.CastleWindsor.Patterns.Component.ComponentFor
{
    internal sealed class ComponentForNonGeneric : ComponentForBase
    {
        // todo there seems to be a bug in resharper that doesn't match open generic types, i.e. typeof(IEnumerable<>)
        // todo changing the pattern to match all arguments, then manually extract the type.
        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$component$.For($service$)",
                                              new ExpressionPlaceholder("component", "Castle.MicroKernel.Registration.Component"),
                                              new ArgumentPlaceholder("service"));

        public ComponentForNonGeneric(params ImplementedByBase[] implementedByPatterns)
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

                    yield return new ConcreteRegistration(typeOfExpression.GetDocumentRange(), typeElement.GetTypeElement());
                }
            }
        }
    }
}
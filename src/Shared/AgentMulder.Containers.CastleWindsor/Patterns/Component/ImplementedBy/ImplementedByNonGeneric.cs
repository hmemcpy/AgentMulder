using System.Collections.Generic;
using AgentMulder.ReSharper.Domain.Patterns;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.CastleWindsor.Patterns.Component.ImplementedBy
{
    internal class ImplementedByNonGeneric : ComponentImplementationPatternBase
    {
        // there seems to be an issue in ReSharper matching open generic types (such as typeof(IEnumerable<>)).
        // changing to match the argument instead, and extract the typeof expression manually

        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$componentFor$.ImplementedBy($impl$)",
                                              new ExpressionPlaceholder("componentFor"),
                                              new ArgumentPlaceholder("impl"));

        public ImplementedByNonGeneric()
            : base(pattern, "impl")
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
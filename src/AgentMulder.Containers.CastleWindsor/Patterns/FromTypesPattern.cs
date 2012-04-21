using System;
using System.Collections.Generic;
using System.Linq;
using AgentMulder.ReSharper.Domain.Registrations;
using AgentMulder.ReSharper.Domain.Search;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.CastleWindsor.Patterns
{
    public class FromTypesPattern : ComponentRegistrationPatternBase
    {
        private static readonly IStructuralSearchPattern pattern = 
            new CSharpStructuralSearchPattern("$alltypes$.From($services$)",
                new ExpressionPlaceholder("alltypes", "Castle.MicroKernel.Registration.AllTypes"),
                new ArgumentPlaceholder("services"));

        public FromTypesPattern()
            : base(pattern)
        {
        }

        public override IComponentRegistrationCreator CreateComponentRegistrationCreator()
        {
            return new FromTypesComponentCreator();
        }

        private sealed class FromTypesComponentCreator : IComponentRegistrationCreator
        {
            public IEnumerable<IComponentRegistration> CreateRegistrations(params IStructuralMatchResult[] matchResults)
            {
                foreach (var match in matchResults)
                {
                    var argument = match.GetMatchedElement("services") as ICSharpArgument;
                    if (argument == null)
                    {
                        yield break;
                    }

                    var arrayExpression = argument.Value as IArrayCreationExpression;
                    if (arrayExpression != null)
                    {
                        foreach (var initializer in arrayExpression.ArrayInitializer.ElementInitializers.OfType<IExpressionInitializer>())
                        {
                            var typeOfExpression = initializer.Value as ITypeofExpression;
                            if (typeOfExpression == null)
                            {
                                // only typeof expressions are currently supported
                                continue;
                            }

                            var typeElement = (IDeclaredType)typeOfExpression.ArgumentType;

                            yield return new ComponentRegistration(match.GetDocumentRange(), typeElement.GetTypeElement());
                        }
                    }
                }
            }
        }

    }
}
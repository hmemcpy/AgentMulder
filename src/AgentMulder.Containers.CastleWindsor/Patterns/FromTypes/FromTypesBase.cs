using System.Collections.Generic;
using System.Linq;
using AgentMulder.ReSharper.Domain.Registrations;
using AgentMulder.ReSharper.Domain.Search;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.Containers.CastleWindsor.Patterns.FromTypes
{
    public abstract class FromTypesBase : RegistrationBase
    {
        private readonly string argumentsElementName;

        protected FromTypesBase(IStructuralSearchPattern pattern, string argumentsElementName)
            : base(pattern)
        {
            this.argumentsElementName = argumentsElementName;
        }

        public override IComponentRegistrationCreator CreateComponentRegistrationCreator()
        {
            return new FromTypesComponentCreator(argumentsElementName);
        }

        private sealed class FromTypesComponentCreator : IComponentRegistrationCreator
        {
            private readonly string elementName;

            public FromTypesComponentCreator(string elementName)
            {
                this.elementName = elementName;
            }

            public IEnumerable<IComponentRegistration> CreateRegistrations(params IStructuralMatchResult[] matchResults)
            {
                foreach (var match in matchResults)
                {
                    var argument = match.GetMatchedElement(elementName) as ICSharpArgument;
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

                    var objectCreationExpression = argument.Value as IObjectCreationExpression;
                    if (objectCreationExpression != null)
                    {
                        
                    }
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using AgentMulder.ReSharper.Domain.Registrations;
using AgentMulder.ReSharper.Domain.Search;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

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

        public override IEnumerable<IComponentRegistration> GetComponentRegistrations(params IStructuralMatchResult[] matchResults)
        {
            IStructuralMatcher matcher = Pattern.CreateMatcher();

            foreach (IStructuralMatchResult match in matchResults)
            {
                foreach (var registration in CreateComponentRegistrations(match.MatchedElement, matcher)) yield return registration;
            }
        }

        private IEnumerable<IComponentRegistration> CreateComponentRegistrations(ITreeNode element, IStructuralMatcher matcher)
        {
            IStructuralMatchResult match = matcher.Match(element);
            if (match.Matched)
            {
                IEnumerable<ICSharpArgument> matchedArguments = match.GetMatchedElementList(argumentsElementName).OfType<ICSharpArgument>();
                foreach (ICSharpArgument argument in matchedArguments)
                {
                    foreach (IComponentRegistration registration in ComponentRegistrations(match, argument.Value))
                    {
                        yield return registration;
                    }
                }
            }
        }

        private static IEnumerable<IComponentRegistration> ComponentRegistrations(IStructuralMatchResult match, ICSharpExpression expression)
        {
            // match typeof() expressions
            var typeOfExpression = expression as ITypeofExpression;
            if (typeOfExpression != null)
            {
                var typeElement = (IDeclaredType)typeOfExpression.ArgumentType;

                yield return new ConcreteRegistration(match.GetDocumentRange(), typeElement.GetTypeElement());
            }

            // match new[] or new Type[] expressions
            var arrayExpression = expression as IArrayCreationExpression;
            if (arrayExpression != null)
            {
                foreach (var initializer in arrayExpression.ArrayInitializer.ElementInitializers.OfType<IExpressionInitializer>())
                {
                    foreach (IComponentRegistration componentRegistration in ComponentRegistrations(match, initializer.Value))
                    {
                        yield return componentRegistration;
                    }
                }
            }

            // match new List<Type> expressions
            var objectCreationExpression = expression as IObjectCreationExpression;
            if (objectCreationExpression != null)
            {
                foreach (var initializer in objectCreationExpression.Initializer.InitializerElements.OfType<ICollectionElementInitializer>())
                {
                    // todo fixme find out if THERE CAN BE ONLY ONE!!!1
                    if (initializer.Arguments.Count != 1)
                    {
                        continue;
                    }

                    foreach (IComponentRegistration componentRegistration in ComponentRegistrations(match, initializer.Arguments[0].Value))
                    {
                        yield return componentRegistration;
                    }
                }
            }
        }
    }
}
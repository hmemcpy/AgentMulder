using System;
using System.Collections.Generic;
using System.Linq;
using AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.BasedOn;
using AgentMulder.ReSharper.Domain.Patterns;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.CastleWindsor.Patterns.FromTypes
{
    public abstract class FromTypesPatternBase : RegistrationPatternBase
    {
        private readonly Predicate<ITypeElement> filter;
        private readonly IEnumerable<IBasedOnPattern> basedOnPatterns;

        protected IEnumerable<IBasedOnPattern> BasedOnPatterns
        {
            get { return basedOnPatterns; }
        }

        protected virtual Predicate<ITypeElement> Filter
        {
            get { return filter; }
        }

        protected FromTypesPatternBase(IStructuralSearchPattern pattern, Predicate<ITypeElement> filter, IEnumerable<IBasedOnPattern> basedOnPatterns)
            : base(pattern)
        {
            this.filter = filter;
            this.basedOnPatterns = basedOnPatterns;
        }

        public override IEnumerable<IComponentRegistration> GetComponentRegistrations(ITreeNode registrationRootElement)
        {
            IStructuralMatchResult match = Match(registrationRootElement);

            if (match.Matched)
            {
                foreach (IBasedOnPattern basedOnPattern in basedOnPatterns)
                {
                    foreach (BasedOnRegistrationBase basedOnRegistration in basedOnPattern.GetComponentRegistrations(registrationRootElement))
                    {
                        IEnumerable<ICSharpArgument> matchedArguments = match.GetMatchedElementList("services").OfType<ICSharpArgument>();
                        IEnumerable<ITypeElement> typeElements = matchedArguments.SelectMany(argument => GetRegisteredTypes(match, argument.Value));

                        basedOnRegistration.AddFilter(Filter);

                        yield return new TypesBasedOnRegistration(typeElements, basedOnRegistration);
                    }
                }
            }
        }

        private static IEnumerable<ITypeElement> GetRegisteredTypes(IStructuralMatchResult match, ICSharpExpression expression)
        {
            // match typeof() expressions
            var typeOfExpression = expression as ITypeofExpression;
            if (typeOfExpression != null)
            {
                var typeElement = (IDeclaredType)typeOfExpression.ArgumentType;

                yield return typeElement.GetTypeElement();
            }

            // match new[] or new Type[] expressions
            var arrayExpression = expression as IArrayCreationExpression;
            if (arrayExpression != null)
            {
                foreach (var initializer in arrayExpression.ArrayInitializer.ElementInitializers.OfType<IExpressionInitializer>())
                {
                    foreach (ITypeElement type in GetRegisteredTypes(match, initializer.Value))
                    {
                        yield return type;
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

                    foreach (ITypeElement type in GetRegisteredTypes(match, initializer.Arguments[0].Value))
                    {
                        yield return type;
                    }
                }
            }
        }
    }
}
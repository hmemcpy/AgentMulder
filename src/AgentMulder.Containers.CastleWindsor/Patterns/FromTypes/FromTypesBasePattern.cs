using System;
using System.Collections.Generic;
using System.Linq;
using AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.BasedOn;
using AgentMulder.ReSharper.Domain.Registrations;
using AgentMulder.ReSharper.Domain.Search;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.CastleWindsor.Patterns.FromTypes
{
    public abstract class FromTypesBasePattern : RegistrationBasePattern
    {
        protected readonly IEnumerable<BasedOnRegistrationBasePattern> basedOnPatterns;

        protected FromTypesBasePattern(IStructuralSearchPattern pattern, IEnumerable<BasedOnRegistrationBasePattern> basedOnPatterns)
            : base(pattern)
        {
            this.basedOnPatterns = basedOnPatterns;
        }

        public override IEnumerable<IComponentRegistration> GetComponentRegistrations(ITreeNode registrationRootElement)
        {
            IStructuralMatchResult match = Match(registrationRootElement);

            if (match.Matched)
            {
                foreach (var basedOnPattern in basedOnPatterns)
                {
                    foreach (var basedOnRegistration in basedOnPattern.GetComponentRegistrations(registrationRootElement).OfType<BasedOnRegistration>())
                    {
                        IEnumerable<ICSharpArgument> matchedArguments = match.GetMatchedElementList("services").OfType<ICSharpArgument>();
                        IEnumerable<ITypeElement> typeElements = matchedArguments.SelectMany(argument => GetRegisteredTypes(match, argument.Value));

                        yield return CreateRegistration(match, basedOnRegistration, typeElements);
                    }
                }
            }
        }

        protected virtual IComponentRegistration CreateRegistration(IStructuralMatchResult match, BasedOnRegistration basedOnRegistration, IEnumerable<ITypeElement> typeElements)
        {
            // get all non-abstract classes (same as AllTypes)
            typeElements = typeElements.OfType<IClass>().Where(c => !c.IsAbstract);

            return new TypesBasedOnRegistration(typeElements, basedOnRegistration);
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
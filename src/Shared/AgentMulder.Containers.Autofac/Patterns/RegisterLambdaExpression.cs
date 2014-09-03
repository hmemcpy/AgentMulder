using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using AgentMulder.ReSharper.Domain.Patterns;
using AgentMulder.ReSharper.Domain.Registrations;
using AgentMulder.ReSharper.Domain.Utils;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Resolve;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.Autofac.Patterns
{
    [Export("ComponentRegistration", typeof(IRegistrationPattern))]
    internal sealed class RegisterLambdaExpression : RegistrationPatternBase
    {
        private readonly IEnumerable<IBasedOnPattern> basedOnPatterns;

        private static readonly IStructuralSearchPattern pattern = 
            new CSharpStructuralSearchPattern("$builder$.Register($args$ => $expression$)",
                new ExpressionPlaceholder("builder", "global::Autofac.ContainerBuilder", true),
                new ArgumentPlaceholder("args", -1, -1),
                new ExpressionPlaceholder("expression"));

        [ImportingConstructor]
        public RegisterLambdaExpression([ImportMany] IEnumerable<IBasedOnPattern> basedOnPatterns)
            : base(pattern)
        {
            this.basedOnPatterns = basedOnPatterns;
        }

        public override IEnumerable<IComponentRegistration> GetComponentRegistrations(ITreeNode registrationRootElement)
        {
            var parentExpression = registrationRootElement.GetParentExpression<IExpressionStatement>();
            if (parentExpression == null)
            {
                yield break;
            }

            IStructuralMatchResult match = Match(registrationRootElement);

            if (match.Matched)
            {
                var expression = match.GetMatchedElement<ICSharpExpression>("expression");

                if (IsResolvedToObject(expression))
                {
                    yield break;
                }

                IEnumerable<IComponentRegistration> componentRegistrations = GetRegistrationsFromExpression(registrationRootElement, expression);

                IEnumerable<FilteredRegistrationBase> basedOnRegistrations = basedOnPatterns.SelectMany(
                   basedOnPattern => basedOnPattern.GetBasedOnRegistrations(parentExpression.Expression)).ToList();

                var registrations = componentRegistrations.Concat(basedOnRegistrations).ToList();

                if (registrations.Any())
                {
                    yield return new CompositeRegistration(registrationRootElement, registrations);
                }
            }
        }

        private bool IsResolvedToObject(ICSharpExpression expression) // I don't have any better ideas at the moment...
        {
            var invocationExpression = expression as IInvocationExpression;
            if (invocationExpression != null)
            {
                if (invocationExpression.Reference != null)
                {
                    var result = invocationExpression.Reference.Resolve().Result.DeclaredElement as IParametersOwner;
                    if (result != null)
                    {
                        return result.ReturnType.IsObject();
                    }
                }
            }
            
            var castExpression = expression as ICastExpression;
            if (castExpression != null)
            {
                var typeUsage = castExpression.TargetType as IPredefinedTypeUsage;
                if (typeUsage != null)
                {
                    return IsReferenceToSystemObject(typeUsage.ScalarPredefinedTypeName);
                }
            }
            var asExpression = expression as IAsExpression;
            if (asExpression != null)
            {
                var typeUsage = asExpression.TypeOperand as IPredefinedTypeUsage;
                if (typeUsage != null)
                {
                    return IsReferenceToSystemObject(typeUsage.ScalarPredefinedTypeName);
                }
            }

            var objectCreationExpression = expression as IObjectCreationExpression;
            if (objectCreationExpression != null)
            {
                return IsReferenceToSystemObject(objectCreationExpression.TypeReference as IPredefinedTypeReference);
            }

            return false;
        }

        private static readonly ClrTypeName clrObjectName = new ClrTypeName("System.Object");

        private static bool IsReferenceToSystemObject(IPredefinedTypeReference reference)
        {
            if (reference != null)
            {
                var result = reference.Reference.Resolve().Result.DeclaredElement as ITypeElement;
                if (result != null)
                {
                    return result.GetClrName().Equals(clrObjectName);
                }
            }
            
            return false;
        }

        private IEnumerable<IComponentRegistration> GetRegistrationsFromExpression(ITreeNode registrationRootElement, ICSharpExpression expression)
        {
            var castExpression = expression as ICastExpression;
            if (castExpression != null)
            {
                var predefinedTypeUsage = castExpression.TargetType as IUserTypeUsage;
                if (predefinedTypeUsage != null && predefinedTypeUsage.ScalarTypeName != null)
                {
                    IResolveResult resolveResult = predefinedTypeUsage.ScalarTypeName.Reference.Resolve().Result;
                    var typeElement = resolveResult.DeclaredElement as ITypeElement;
                    if (typeElement != null)
                    {
                        yield return new ServiceRegistration(registrationRootElement, typeElement);
                    }
                }
            }

            var asExpression = expression as IAsExpression;
            if (asExpression != null)
            {
                var typeUsage = asExpression.TypeOperand as IUserTypeUsage;
                if (typeUsage != null && typeUsage.ScalarTypeName != null)
                {
                    IResolveResult resolveResult = typeUsage.ScalarTypeName.Reference.Resolve().Result;
                    var typeElement = resolveResult.DeclaredElement as ITypeElement;
                    if (typeElement != null)
                    {
                        yield return new ServiceRegistration(registrationRootElement, typeElement);
                    }
                }
            }

            var objectCreationExpression = expression as IObjectCreationExpression;
            if (objectCreationExpression != null && objectCreationExpression.TypeReference != null)
            {
                IResolveResult resolveResult = objectCreationExpression.TypeReference.Resolve().Result;

                var @class = resolveResult.DeclaredElement as IClass;
                if (@class != null)
                {
                    yield return new ComponentRegistration(registrationRootElement, @class, @class);
                }
            }

            var invocationExpression = expression as IInvocationExpression;
            if (invocationExpression != null)
            {
                if (invocationExpression.Reference != null)
                {
                    var result = invocationExpression.Reference.Resolve().Result.DeclaredElement as IParametersOwner;
                    if (result != null)
                    {
                        var type = result.ReturnType as IDeclaredType;
                        if (type != null)
                        {
                            var @class = type.GetTypeElement();
                            if (@class != null)
                            {
                                yield return new ComponentRegistration(registrationRootElement, @class, @class);
                            }
                        }
                    }
                }
            }
        }
    }
}
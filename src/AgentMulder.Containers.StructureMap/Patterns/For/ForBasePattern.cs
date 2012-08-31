using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using AgentMulder.ReSharper.Domain.Patterns;
using AgentMulder.ReSharper.Domain.Registrations;
using AgentMulder.ReSharper.Domain.Utils;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.StructureMap.Patterns.For
{
    [InheritedExport("ComponentRegistration", typeof(IRegistrationPattern))]
    internal abstract class ForBasePattern : ComponentRegistrationPatternBase
    {
        private readonly IEnumerable<ComponentImplementationPatternBase> usePatterns;
        private IDeclaredType bindingRootType;

        protected ForBasePattern(IStructuralSearchPattern pattern, string elementName, IEnumerable<ComponentImplementationPatternBase> usePatterns)
            : base(pattern, elementName)
        {
            this.usePatterns = usePatterns;
        }

        public override IEnumerable<IComponentRegistration> GetComponentRegistrations(ITreeNode registrationRootElement)
        {
            // todo this is duplicated from Ninject!
            // This entire thing is one big hack. Need to come back to it one day :)
            // There is (currently) no way to create a pattern that would match the For() call with implicit 'this' in ReSharper SSR.
            // Therefore I'm only matching by the method name only, and later verifying that the method indeed belongs to StructureMap, by
            // making sure the invocation's qualifier derived from global::StructureMap.Configuration.DSL.IRegistry

            if (!IsStructureMapCall(registrationRootElement))
            {
                yield break;
            }


            IInvocationExpression invocationExpression = GetInvocationExpression(registrationRootElement);
            if (invocationExpression == null)
            {
                yield break;
            }

            foreach (var usePattern in usePatterns)
            {
                var implementedByRegistration = usePattern.GetComponentRegistrations(invocationExpression)
                    .Cast<ComponentRegistration>()
                    .FirstOrDefault();

                if (implementedByRegistration != null)
                {
                    foreach (var registration in DoCreateRegistrations(invocationExpression).OfType<ComponentRegistration>())
                    {
                        registration.Implementation = implementedByRegistration.ServiceType;
                        yield return registration;
                    }
                }
            }
        }

        private bool IsStructureMapCall(ITreeNode element)
        {
            var invocationExpression = element as IInvocationExpression;
            if (invocationExpression == null)
            {
                return false;
            }

            var resolve = invocationExpression.InvocationExpressionReference.Resolve().Result;
            var method = resolve.DeclaredElement as IMethod;
            if (method == null)
            {
                return false;
            }
            ITypeElement containingType = method.GetContainingType();
            if (containingType == null)
            {
                return false;
            }

            if (bindingRootType == null)
            {
                bindingRootType = TypeFactory.CreateTypeByCLRName("StructureMap.Configuration.DSL.IRegistry", element.GetPsiModule());
            }

            return containingType.IsDescendantOf(bindingRootType.GetTypeElement());

        }

        private IInvocationExpression GetInvocationExpression(ITreeNode node)
        {
            // first, try to find the parent statement expression
            var statement = node.GetParentExpression<IExpressionStatement>();
            if (statement != null)
            {
                return statement.Expression as IInvocationExpression;
            }

            // otherwise, try finding the lambda expression, and take its invocation
            var lambdaExpression = node.GetParentExpression<ILambdaExpression>();
            if (lambdaExpression != null)
            {
                return lambdaExpression.BodyExpression as IInvocationExpression;
            }

            return null;
        }

        protected virtual IEnumerable<IComponentRegistration> DoCreateRegistrations(ITreeNode parentElement)
        {
            return base.GetComponentRegistrations(parentElement);
        }
    }
}
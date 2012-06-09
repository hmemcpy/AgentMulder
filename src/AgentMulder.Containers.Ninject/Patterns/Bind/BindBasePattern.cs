using System.Collections.Generic;
using System.Linq;
using AgentMulder.ReSharper.Domain.Patterns;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.Ninject.Patterns.Bind
{
    internal abstract class BindBasePattern : ComponentRegistrationPatternBase
    {
        private readonly IEnumerable<ComponentRegistrationPatternBase> toPatterns;
        private IDeclaredType bindingRootType;

        protected BindBasePattern(IStructuralSearchPattern pattern, string elementName, IEnumerable<ComponentRegistrationPatternBase> toPatterns)
            : base(pattern, elementName)
        {
            this.toPatterns = toPatterns;
        }

        public override IEnumerable<IComponentRegistration> GetComponentRegistrations(ITreeNode registrationRootElement)
        {
            // This entire thing is one big hack. Need to come back to it one day :)
            // There is (currently) no way to create a pattern that would match the Bind() call with implicit this in ReSharper SSR.
            // Therefore I'm only matching by the method name only, and later verifying that the method invocation's qualifier 
            // is indeed derived from global::Ninject.Syntax.BindingRoot

            if (!IsNinjectBindCall(registrationRootElement))
            {
                yield break;
            }

            IExpressionStatement statement = GetParentExpressionStatemenmt(registrationRootElement);
            if (statement == null)
            {
                yield break;
            }
            foreach (var toPattern in toPatterns)
            {
                var implementedByRegistration = toPattern.GetComponentRegistrations(statement.Expression)
                    .Cast<ComponentRegistration>()
                    .FirstOrDefault();

                if (implementedByRegistration != null)
                {
                    foreach (var registration in DoCreateRegistrations(statement.Expression).OfType<ComponentRegistration>())
                    {
                        registration.Implementation = implementedByRegistration.ServiceType;
                        yield return registration;
                    }
                }
            }
        }

        private IExpressionStatement GetParentExpressionStatemenmt(ITreeNode node)
        {
            for (var n = node; n != null; n = n.Parent)
            {
                var expressionStatement = n as IExpressionStatement;
                if (expressionStatement != null)
                    return expressionStatement;
            }

            return null;
        }

        private bool IsNinjectBindCall(ITreeNode element)
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
                bindingRootType = TypeFactory.CreateTypeByCLRName("Ninject.Syntax.BindingRoot", element.GetPsiModule());
            }

            return containingType.Equals(bindingRootType.GetTypeElement());
        }

        protected virtual IEnumerable<IComponentRegistration> DoCreateRegistrations(ITreeNode parentElement)
        {
            return base.GetComponentRegistrations(parentElement);
        }
    }
}
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using AgentMulder.ReSharper.Domain.Patterns;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.Metadata.Reader.Impl;
using JetBrains.ReSharper.Feature.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Feature.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Feature.Services.StructuralSearch;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.Catel.Patterns
{
    using System;

    [InheritedExport("ComponentRegistration", typeof(IRegistrationPattern))]
    public abstract class ServiceLocatorRegistrationPatternBase : ComponentRegistrationPatternBase
    {
        private static readonly ClrTypeName clrTypeName = new ClrTypeName("System.Type");

        protected ServiceLocatorRegistrationPatternBase(string methodName)
            : base(
                new CSharpStructuralSearchPattern(
                    $"$container$.{methodName}($arguments$)", 
                    new ExpressionPlaceholder("container", "global::Catel.IoC.IServiceLocator", false), 
                    new ArgumentPlaceholder("arguments", -1, -1)), 
                string.Empty)
        {
        }

        public override IEnumerable<IComponentRegistration> GetComponentRegistrations(ITreeNode registrationRootElement)
        {
            IStructuralMatchResult match = Match(registrationRootElement);

            if (match.Matched)
            {
                var invocationExpression = match.MatchedElement as IInvocationExpression;
                if (invocationExpression == null)
                {
                    yield break;
                }

                if (invocationExpression.TypeArguments.Count > 0)
                {
                    foreach (IComponentRegistration registration in FromGenericArguments(invocationExpression))
                    {
                        yield return registration;
                    }
                }
                else
                {
                    foreach (IComponentRegistration registration in FromArguments(invocationExpression))
                    {
                        yield return registration;
                    }
                }
            }
        }

        private static IEnumerable<IComponentRegistration> CreateRegistration(
            IInvocationExpression invocationExpression, IDeclaredType first, IDeclaredType last)
        {
            if (first == null || last == null)
            {
                yield break;
            }

            ITypeElement fromType = first.GetTypeElement();
            ITypeElement toType = last.GetTypeElement();

            if (fromType != null && toType != null)
            {
                yield return
                    fromType.Equals(toType)
                        ? new ComponentRegistration(invocationExpression, fromType)
                        : new ComponentRegistration(invocationExpression, fromType, toType);
            }
        }

        private static IEnumerable<IComponentRegistration> FromGenericArguments(
            IInvocationExpression invocationExpression)
        {
            var first = invocationExpression.TypeArguments.First() as IDeclaredType;
            var last = invocationExpression.TypeArguments.Last() as IDeclaredType;

            return CreateRegistration(invocationExpression, first, last);
        }

        private IEnumerable<IComponentRegistration> FromArguments(IInvocationExpression invocationExpression)
        {
            List<ITypeofExpression> arguments = invocationExpression.ArgumentList.Arguments.Where(
                argument =>
                {
                    var declaredType = argument.Value.Type() as IDeclaredType;
                    return declaredType != null && declaredType.GetClrName().Equals(clrTypeName);
                }).Select(argument => argument.Value as ITypeofExpression).ToList();

            var first = arguments.First().ArgumentType as IDeclaredType;
            var last = arguments.Last().ArgumentType as IDeclaredType;

            return CreateRegistration(invocationExpression, first, last);
        }

    }
}
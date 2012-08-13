// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceLocatorRegistrationPatternBase.cs" company="Catel & Agent Mulder development teams">
//   Copyright (c) 2008 - 2012 Catel & Agent Mulder development teams. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace AgentMulder.Containers.Catel.Patterns
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AgentMulder.ReSharper.Domain.Patterns;
    using AgentMulder.ReSharper.Domain.Registrations;

    using JetBrains.ReSharper.Psi;
    using JetBrains.ReSharper.Psi.CSharp.Tree;
    using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
    using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
    using JetBrains.ReSharper.Psi.Services.StructuralSearch;
    using JetBrains.ReSharper.Psi.Tree;

    /// <summary>
    /// The service locator registration pattern base.
    /// </summary>
    public class ServiceLocatorRegistrationPatternBase : ComponentRegistrationPatternBase
    {
        #region Constants and Fields

        /// <summary>
        /// The clr type name.
        /// </summary>
        private static readonly ClrTypeName clrTypeName = new ClrTypeName("System.Type");

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceLocatorRegistrationPatternBase"/> class. 
        /// </summary>
        /// <param name="methodName">
        /// The method name.
        /// </param>
        protected ServiceLocatorRegistrationPatternBase(string methodName)
            : base(
                new CSharpStructuralSearchPattern(
                    string.Format("$container$.{0}($arguments$)", methodName), 
                    new ExpressionPlaceholder("container", "Catel.IoC.IServiceLocator", false), 
                    new ArgumentPlaceholder("arguments", -1, -1)), 
                string.Empty)
        {
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The get component registrations.
        /// </summary>
        /// <param name="registrationRootElement">
        /// The registration root element.
        /// </param>
        /// <returns>
        /// </returns>
        public override IEnumerable<IComponentRegistration> GetComponentRegistrations(ITreeNode registrationRootElement)
        {
            IStructuralMatchResult match = this.Match(registrationRootElement);

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
                    foreach (IComponentRegistration registration in this.FromArguments(invocationExpression))
                    {
                        yield return registration;
                    }
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The create registration.
        /// </summary>
        /// <param name="invocationExpression">
        /// The invocation expression.
        /// </param>
        /// <param name="first">
        /// The first.
        /// </param>
        /// <param name="last">
        /// The last.
        /// </param>
        /// <returns>
        /// </returns>
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

        /// <summary>
        /// The from generic arguments.
        /// </summary>
        /// <param name="invocationExpression">
        /// The invocation expression.
        /// </param>
        /// <returns>
        /// </returns>
        private static IEnumerable<IComponentRegistration> FromGenericArguments(
            IInvocationExpression invocationExpression)
        {
            var first = invocationExpression.TypeArguments.First() as IDeclaredType;
            var last = invocationExpression.TypeArguments.Last() as IDeclaredType;

            return CreateRegistration(invocationExpression, first, last);
        }

        /// <summary>
        /// The from arguments.
        /// </summary>
        /// <param name="invocationExpression">
        /// The invocation expression.
        /// </param>
        /// <returns>
        /// </returns>
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

        #endregion
    }
}
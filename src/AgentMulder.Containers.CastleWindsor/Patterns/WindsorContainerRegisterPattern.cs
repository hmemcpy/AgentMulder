using System;
using System.Collections.Generic;
using System.Linq;
using AgentMulder.ReSharper.Domain.Patterns;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Feature.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Feature.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Feature.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.CastleWindsor.Patterns
{
    internal sealed class WindsorContainerRegisterPattern : RegistrationPatternBase
    {
        private readonly IRegistrationPattern[] argumentsPatterns;

        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$container$.Register($arguments$)",
                                              new ExpressionPlaceholder("container", "Castle.Windsor.IWindsorContainer", false),
                                              new ArgumentPlaceholder("arguments", -1, -1)); // any number of arguments

        public WindsorContainerRegisterPattern(params IRegistrationPattern[] argumentsPatterns)
            : base(pattern)
        {
            this.argumentsPatterns = argumentsPatterns;
        }

        public override IEnumerable<IComponentRegistration> GetComponentRegistrations(ITreeNode registrationRootElement)
        {
            IStructuralMatchResult match = Match(registrationRootElement);

            if (match.Matched)
            {
                IEnumerable<IInvocationExpression> invocationExpressions =
                    match.GetMatchedElementList("arguments").Cast<ICSharpArgument>()
                        .Select(argument => argument.Value)
                        .OfType<IInvocationExpression>().ToList();

                return from argumentPattern in argumentsPatterns
                       from expression in invocationExpressions
                       from registration in argumentPattern.GetComponentRegistrations(expression)
                       select registration;
            }

            return Enumerable.Empty<IComponentRegistration>();
        }
    }
}
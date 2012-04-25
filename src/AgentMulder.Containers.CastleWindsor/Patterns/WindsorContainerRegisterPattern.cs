using System;
using System.Collections.Generic;
using System.Linq;
using AgentMulder.Containers.CastleWindsor.Patterns.FromTypes;
using AgentMulder.ReSharper.Domain.Registrations;
using AgentMulder.ReSharper.Domain.Search;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.Containers.CastleWindsor.Patterns
{
    internal sealed class WindsorContainerRegisterPattern : RegistrationBase
    {
        private readonly RegistrationBase[] argumentsPatterns;

        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$container$.Register($arguments$)",
                new ExpressionPlaceholder("container", "Castle.Windsor.IWindsorContainer", false),
                new ArgumentPlaceholder("arguments", -1, -1)); // any number of arguments

        public WindsorContainerRegisterPattern(params RegistrationBase[] argumentsPatterns)
            : base(pattern)
        {
            this.argumentsPatterns = argumentsPatterns;
        }

        public override IEnumerable<IComponentRegistration> GetComponentRegistrations(params IStructuralMatchResult[] matchResults)
        {
            foreach (IStructuralMatchResult match in matchResults)
            {
                foreach (ICSharpArgument argument in match.GetMatchedElementList("arguments").OfType<ICSharpArgument>())
                {
                    foreach (RegistrationBase argumentPattern in argumentsPatterns)
                    {
                        foreach (IComponentRegistration registration in GetComponentRegistration(argument.Value, argumentPattern))
                        {
                            yield return registration;
                        }
                    }
                }
            }
        }

        private static IEnumerable<IComponentRegistration> GetComponentRegistration(ICSharpExpression expression, RegistrationBase argumentPattern)
        {
            IElementMatcher elementMatcher = new InvocationExpressionMatcher(
                (IInvocationExpression)expression, new PatternMatcherBuilderParams(argumentPattern.Pattern.Params));
            IStructuralMatcher matcher = new CSharpExpressionStructuralMatcher(elementMatcher);

            IStructuralMatchResult result = matcher.Match(expression);

            if (result.Matched)
            {
                foreach (var registration in argumentPattern.GetComponentRegistrations(result))
                {
                    yield return registration;
                }
            }
        }
    }
}

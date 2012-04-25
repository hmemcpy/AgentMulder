using System;
using System.Collections.Generic;
using System.Linq;
using AgentMulder.ReSharper.Domain.Registrations;
using AgentMulder.ReSharper.Domain.Search;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.Containers.CastleWindsor.Patterns
{
    internal sealed class WindsorContainerRegisterPattern : RegistrationBase
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

        public override IEnumerable<IComponentRegistration> GetComponentRegistrations(params IStructuralMatchResult[] matchResults)
        {
            foreach (IStructuralMatchResult match in matchResults)
            {
                foreach (ICSharpArgument argument in match.GetMatchedElementList("arguments").OfType<ICSharpArgument>())
                {
                    foreach (IRegistrationPattern argumentPattern in argumentsPatterns)
                    {
                        foreach (IComponentRegistration registration in GetComponentRegistration(argument.Value as IInvocationExpression, argumentPattern))
                        {
                            yield return registration;
                        }
                    }
                }
            }
        }

        private static IEnumerable<IComponentRegistration> GetComponentRegistration(IInvocationExpression expression, IRegistrationPattern argumentPattern)
        {
            IStructuralMatcher argumentMatcher = argumentPattern.CreateMatcher();

            // todo fixme aaarrrgghh
            IStructuralMatchResult result = argumentMatcher.Match(expression);
            
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

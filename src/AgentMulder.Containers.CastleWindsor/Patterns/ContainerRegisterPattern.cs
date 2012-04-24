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
    internal sealed class ContainerRegisterPattern : RegistrationBase
    {
        private readonly IRegistrationPattern[] argumentsPatterns;

        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$container$.Register($arguments$)",
                new ExpressionPlaceholder("container", "Castle.Windsor.IWindsorContainer"),
                new ArgumentPlaceholder("arguments", -1, -1)); // any number of arguments

        public ContainerRegisterPattern(params IRegistrationPattern[] argumentsPatterns)
            : base(pattern)
        {
            this.argumentsPatterns = argumentsPatterns;
        }

        public override IEnumerable<IComponentRegistration> GetComponentRegistrations(params IStructuralMatchResult[] matchResults)
        {
            // todo yeah...
            foreach (IStructuralMatchResult match in matchResults)
            {
                foreach (ICSharpArgument argument in match.GetMatchedElementList("arguments").OfType<ICSharpArgument>())
                {
                    foreach (var argumentPattern in argumentsPatterns)
                    {
                        foreach (var componentRegistration in GetComponentRegistration(argument.Value, argumentPattern))
                        {
                            yield return componentRegistration;
                        }
                    }
                }
            }
        }

        private static IEnumerable<IComponentRegistration> GetComponentRegistration(ICSharpExpression argument, IRegistrationPattern argumentPattern)
        {
            IStructuralMatcher argumentMatcher = argumentPattern.CreateMatcher();
            IStructuralMatchResult result = argumentMatcher.Match(argument);
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
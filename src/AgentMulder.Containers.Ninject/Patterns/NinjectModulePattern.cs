using System;
using System.Collections.Generic;
using System.Linq;
using AgentMulder.ReSharper.Domain.Patterns;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.Ninject.Patterns
{
    public class NinjectModulePattern : RegistrationPatternBase
    {
        private readonly IRegistrationPattern[] bindPatterns;

        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("class $module$ : NinjectModule { public override void Load() { $statements$ } }",
                new IdentifierPlaceholder("module"),
                new StatementPlaceholder("statements", -1, -1));

        public NinjectModulePattern(params IRegistrationPattern[] bindPatterns)
            : base(pattern)
        {
            this.bindPatterns = bindPatterns;
        }

        public override IEnumerable<IComponentRegistration> GetComponentRegistrations(ITreeNode registrationRootElement)
        {
            IStructuralMatchResult match = Match(registrationRootElement);

            if (match.Matched)
            {
                IEnumerable<IInvocationExpression> invocationExpressions =
                    match.GetMatchedElementList("statements").Cast<IExpressionStatement>().Select(statement => statement.Expression as IInvocationExpression);

                return from bindPattern in bindPatterns
                       from invocationExpression in invocationExpressions
                       from registration in bindPattern.GetComponentRegistrations(invocationExpression)
                       select registration;
            }

            return Enumerable.Empty<IComponentRegistration>();
        }
    }
}
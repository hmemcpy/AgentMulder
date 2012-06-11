using System.Collections.Generic;
using System.Linq;
using AgentMulder.Containers.Autofac.Patterns.Helpers;
using AgentMulder.ReSharper.Domain.Patterns;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.CSharp.Util;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Resolve.Managed;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Util;
using ReturnTypeCollector = AgentMulder.Containers.Autofac.Patterns.Helpers.ReturnTypeCollector;

namespace AgentMulder.Containers.Autofac.Patterns
{
    internal sealed class RegisterLambdaStatements : RegistrationPatternBase
    {
        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$builder$.Register($args$ => { $statements$ })",
                new ExpressionPlaceholder("builder", "global::Autofac.ContainerBuilder", true),
                new ArgumentPlaceholder("args", -1, -1),
                new StatementPlaceholder("statements", -1, -1));

        public RegisterLambdaStatements()
            : base(pattern)
        {
        }

        public override IEnumerable<IComponentRegistration> GetComponentRegistrations(ITreeNode registrationRootElement)
        {
            IStructuralMatchResult match = Match(registrationRootElement);

            if (match.Matched)
            {
                var statements = match.GetMatchedElementList("statements").Cast<ICSharpStatement>();

                var collectedTypes = statements.SelectMany(statement =>
                {
                    var returnTypeCollector = new ReturnTypeCollector(new UniversalContext(statement.GetPsiModule()));
                    statement.ProcessDescendants(returnTypeCollector);
                    return returnTypeCollector.CollectedTypes;
                });

                foreach (var type in collectedTypes)
                {
                    var declaredType = type as IDeclaredType;
                    if (declaredType != null)
                    {
                        var typeElement = declaredType.GetTypeElement();
                        if (typeElement != null)
                        {
                            yield return new ElementBasedOnRegistration(registrationRootElement, typeElement);
                        }
                    }
                }
            }
        }
    }
}
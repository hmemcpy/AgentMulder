using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using AgentMulder.Containers.Autofac.Patterns.Helpers;
using AgentMulder.ReSharper.Domain.Patterns;
using AgentMulder.ReSharper.Domain.Registrations;
using AgentMulder.ReSharper.Domain.Utils;
using JetBrains.ReSharper.Feature.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Feature.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Feature.Services.StructuralSearch;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Resolve.Managed;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.Autofac.Patterns
{
    [Export("ComponentRegistration", typeof(IRegistrationPattern))]
    internal sealed class RegisterLambdaStatements : RegistrationPatternBase
    {
        private readonly IEnumerable<IBasedOnPattern> basedOnPatterns;

        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$builder$.Register($args$ => { $statements$ })",
                new ExpressionPlaceholder("builder", "global::Autofac.ContainerBuilder", true),
                new ArgumentPlaceholder("args", -1, -1),
                new StatementPlaceholder("statements", -1, -1));

        [ImportingConstructor]
        public RegisterLambdaStatements([ImportMany] IEnumerable<IBasedOnPattern> basedOnPatterns)
            : base(pattern)
        {
            this.basedOnPatterns = basedOnPatterns;
        }

        public override IEnumerable<IComponentRegistration> GetComponentRegistrations(ITreeNode registrationRootElement)
        {
            var parentExpression = registrationRootElement.GetParentExpression<IExpressionStatement>();
            if (parentExpression == null)
            {
                yield break;
            }

            IStructuralMatchResult match = Match(registrationRootElement);

            if (match.Matched)
            {
                var statements = match.GetMatchedElementList("statements").Cast<ICSharpStatement>().ToList();
                var collectedTypes = statements.SelectMany(statement =>
                {
                    var returnTypeCollector = new ReturnTypeCollector(new UniversalContext(statement.GetPsiModule()));
                    statement.ProcessThisAndDescendants(returnTypeCollector);
                    return returnTypeCollector.CollectedTypes;
                });

                IEnumerable<FilteredRegistrationBase> basedOnRegistrations = basedOnPatterns.SelectMany(
                   basedOnPattern => basedOnPattern.GetBasedOnRegistrations(parentExpression.Expression)).ToList();

                foreach (var type in collectedTypes)
                {
                    var declaredType = type as IDeclaredType;
                    if (declaredType != null)
                    {
                        var typeElement = declaredType.GetTypeElement();
                        if (typeElement != null)
                        {
                            yield return new CompositeRegistration(registrationRootElement,
                                new[] { new ServiceRegistration(registrationRootElement, typeElement) }
                                .Concat(basedOnRegistrations));
                        }
                    }
                }
            }
        }
    }
}
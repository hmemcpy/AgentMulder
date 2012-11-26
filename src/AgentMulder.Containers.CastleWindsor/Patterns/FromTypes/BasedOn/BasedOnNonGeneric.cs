using System.Collections.Generic;
using AgentMulder.ReSharper.Domain.Patterns;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.BasedOn
{
    internal sealed class BasedOnNonGeneric : BasedOnPatternBase
    {
        private readonly IBasedOnRegistrationCreator registrationCreator;

        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$fromDescriptor$.BasedOn($argument$)",
                new ExpressionPlaceholder("fromDescriptor", "Castle.MicroKernel.Registration.FromDescriptor", false),
                new ArgumentPlaceholder("argument", 1, 1));

        public BasedOnNonGeneric(IBasedOnRegistrationCreator registrationCreator)
            : base(pattern)
        {
            this.registrationCreator = registrationCreator;
        }

        public override IEnumerable<IComponentRegistration> GetComponentRegistrations(ITreeNode registrationRootElement)
        {
            return GetBasedOnRegistrations(registrationRootElement);
        }

        public override IEnumerable<FilteredRegistrationBase> GetBasedOnRegistrations(ITreeNode registrationRootElement)
        {
            IStructuralMatchResult match = Match(registrationRootElement);

            if (match.Matched)
            {
                var argument = match.GetMatchedElement("argument") as ICSharpArgument;
                if (argument == null)
                {
                    yield break;
                }

                var typeofExpression = argument.Value as ITypeofExpression;
                if (typeofExpression != null)
                {
                    var declaredType = typeofExpression.ArgumentType as IDeclaredType;
                    if (declaredType != null)
                    {
                        ITypeElement typeElement = declaredType.GetTypeElement();
                        if (typeElement != null)
                        {
                            yield return registrationCreator.Create(registrationRootElement, typeElement);
                        }
                    }
                }
            }
        }
    }
}
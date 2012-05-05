using System;
using System.Collections.Generic;
using System.Linq;
using AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.WithService;
using AgentMulder.ReSharper.Domain;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Impl;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.BasedOn
{
    internal sealed class Where : BasedOnRegistrationBasePattern
    {
        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$fromDescriptor$.Where($predicate$)",
                new ExpressionPlaceholder("fromDescriptor", "Castle.MicroKernel.Registration.FromDescriptor", false),
                new ExpressionPlaceholder("predicate"));
        
        public Where(params WithServiceRegistrationBasePattern[] withServicePatterns)
            : base(pattern, withServicePatterns)
        {
        }

        public override IEnumerable<IComponentRegistration> GetComponentRegistrations(ITreeNode parentElement)
        {
            IStructuralMatchResult match = Match(parentElement);

            if (match.Matched)
            {

                var lambdaExpression = match.GetMatchedElement("predicate") as ILambdaExpression;
                if (lambdaExpression != null)
                {
                    // get all type elements in a target module
                    // build a predicate
                    // match target element with predicate

                    Predicate<Type> predicate = WherePredicateBuilder.FromLambda<Type>(lambdaExpression);

                    var withServiceRegistrations = base.GetComponentRegistrations(parentElement).OfType<WithServiceRegistration>();

                    yield return new TypePredicateRegistration(match.MatchedElement.GetDocumentRange(), predicate, withServiceRegistrations);
                }
            }
        }
    }
}
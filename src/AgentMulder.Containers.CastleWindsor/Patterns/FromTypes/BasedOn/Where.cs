using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.WithService;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Psi.Resolve.ExtensionMethods;

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
                ITreeNode element = match.GetMatchedElement("predicate");
                var lambdaExpression = element as ILambdaExpression;
                if (lambdaExpression != null)
                {
                    yield break;
                    //yield return FromLambdaExpression(parentElement, lambdaExpression);
                }

                var referenceExpression = element as IReferenceExpression;
                if (referenceExpression != null)
                {
                    yield return FromReferenceExpression(parentElement, referenceExpression);
                }
            }
        }

        private IComponentRegistration FromReferenceExpression(ITreeNode parentElement, IReferenceExpression referenceExpression)
        {
            var resolveResult = referenceExpression.Reference.Resolve().Result;
            ExtensionInstance<IDeclaredElement> extension = resolveResult.ElementAsExtension();
            

            return null;
        }

        private IComponentRegistration FromLambdaExpression(ITreeNode parentElement, ILambdaExpression lambdaExpression)
        {
            throw new NotImplementedException();
            // get all type elements in a target module
            // build a predicate
            // match target element with predicate

            //Expression<Predicate<Type>> predicate = WherePredicateBuilder.FromLambda<Type>(lambdaExpression);

            //var withServiceRegistrations = base.GetComponentRegistrations(parentElement).OfType<WithServiceRegistration>();

            //return new TypePredicateRegistration(parentElement, predicate, withServiceRegistrations);
        }
    }
}
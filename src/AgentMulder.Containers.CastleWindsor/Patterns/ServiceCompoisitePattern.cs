using System;
using System.Collections.Generic;
using AgentMulder.ReSharper.Domain.Registrations;
using AgentMulder.ReSharper.Domain.Search;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.CastleWindsor.Patterns
{
    internal class ServiceCompoisitePattern : ServiceRegistrationPattern
    {
        private readonly IComponentRegistrationPattern withImplementationPattern;

        public ServiceCompoisitePattern()
        {
            withImplementationPattern = new ServiceWithImplementationRegistrationPattern();
        }

        public override IComponentRegistrationCreator CreateComponentRegistrationCreator()
        {
            IComponentRegistrationCreator baseCreator = base.CreateComponentRegistrationCreator();

            return new CompositeCreator(baseCreator, withImplementationPattern);
        }

        private sealed class CompositeCreator : IComponentRegistrationCreator
        {
            private readonly IComponentRegistrationCreator baseCreator;
            private readonly IComponentRegistrationPattern otherPattern;

            public CompositeCreator(IComponentRegistrationCreator baseCreator, IComponentRegistrationPattern otherPattern)
            {
                this.baseCreator = baseCreator;
                this.otherPattern = otherPattern;
            }

            public IEnumerable<IComponentRegistration> CreateRegistrations(params IStructuralMatchResult[] matchResults)
            {
                IStructuralMatcher otherMatcher = otherPattern.CreateMatcher();

                var results = new List<IComponentRegistration>();

                foreach (IStructuralMatchResult match in matchResults)
                {
                    // todo hack fix this 
                    IInvocationExpression parentExpression = FindParentExpression(match.MatchedElement);
                    if (parentExpression != null)
                    {
                        IStructuralMatchResult otherMatchResult = otherMatcher.Match(parentExpression);
                        if (otherMatchResult.Matched)
                        {
                            IComponentRegistrationCreator creator = otherPattern.CreateComponentRegistrationCreator();
                            results.AddRange(creator.CreateRegistrations(otherMatchResult));
                            continue;
                        }
                    }
                    
                    results.AddRange(baseCreator.CreateRegistrations(match));
                }

                return results;
            }

            private IInvocationExpression FindParentExpression(ITreeNode matchedElement)
            {
                for (ITreeNode parent = matchedElement.Parent; parent != null; parent = parent.Parent)
                {
                    var invocationExpression = parent as IInvocationExpression;
                    if (invocationExpression != null)
                    {
                        return invocationExpression;
                    }
                }

                return null;
            }
        }
    }
}
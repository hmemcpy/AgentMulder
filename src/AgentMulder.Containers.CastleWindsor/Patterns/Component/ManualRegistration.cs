using System;
using System.Collections.Generic;
using AgentMulder.ReSharper.Domain.Registrations;
using AgentMulder.ReSharper.Domain.Search;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.CastleWindsor.Patterns.Component
{
    internal sealed class ManualRegistration : RegistrationBase
    {
        private static readonly ComponentForGeneric componentForPattern = new ComponentForGeneric();
        private readonly IRegistration implementedByPattern;

        public ManualRegistration()
            :base(componentForPattern.Pattern)
        {
            implementedByPattern = new ImplementedByGeneric();
        }

        public override IComponentRegistrationCreator CreateComponentRegistrationCreator()
        {
            IComponentRegistrationCreator componentForCreator = componentForPattern.CreateComponentRegistrationCreator();

            return new CompositeCreator(componentForCreator, implementedByPattern);
        }

        private sealed class ComponentForGeneric : ManualRegistrationBase
        {
            private static readonly IStructuralSearchPattern pattern =
                new CSharpStructuralSearchPattern("$component$.For<$service$>()",
                                                  new ExpressionPlaceholder("component", "Castle.MicroKernel.Registration.Component"),
                                                  new TypePlaceholder("service"));

            public ComponentForGeneric()
                : base(pattern, "service")
            {
            }

            public IStructuralSearchPattern Pattern
            {
                get { return pattern; }
            }
        }

        private sealed class ImplementedByGeneric : ManualRegistrationBase
        {
            private static readonly IStructuralSearchPattern pattern =
                new CSharpStructuralSearchPattern("$anything$.ImplementedBy<$impl$>()",
                                                  new ExpressionPlaceholder("anything"),
                                                  new TypePlaceholder("impl"));

            public ImplementedByGeneric()
                : base(pattern, "impl")
            {
            }
        }

        private sealed class CompositeCreator : IComponentRegistrationCreator
        {
            private readonly IComponentRegistrationCreator componentForCreator;
            private readonly IRegistration implementedByPattern;

            public CompositeCreator(IComponentRegistrationCreator componentForCreator, IRegistration implementedByPattern)
            {
                this.componentForCreator = componentForCreator;
                this.implementedByPattern = implementedByPattern;
            }

            public IEnumerable<IComponentRegistration> CreateRegistrations(params IStructuralMatchResult[] matchResults)
            {
                var results = new List<IComponentRegistration>();
                IStructuralMatcher otherMatcher = implementedByPattern.CreateMatcher();
                
                foreach (IStructuralMatchResult match in matchResults)
                {
                    IInvocationExpression parentExpression = FindParentExpression(match.MatchedElement);
                    if (parentExpression != null)
                    {
                        IStructuralMatchResult otherMatchResult = otherMatcher.Match(parentExpression);
                        if (otherMatchResult.Matched)
                        {
                            IComponentRegistrationCreator creator = implementedByPattern.CreateComponentRegistrationCreator();
                            results.AddRange(creator.CreateRegistrations(otherMatchResult));
                            continue;
                        }
                    }
                    
                    results.AddRange(componentForCreator.CreateRegistrations(match));
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
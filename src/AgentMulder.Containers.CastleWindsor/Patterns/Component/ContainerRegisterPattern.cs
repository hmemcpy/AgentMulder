using System;
using System.Collections.Generic;
using System.Linq;
using AgentMulder.ReSharper.Domain.Registrations;
using AgentMulder.ReSharper.Domain.Search;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.Containers.CastleWindsor.Patterns.Component
{
    internal sealed class ContainerRegisterPattern : RegistrationBase
    {
        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$container$.Register($arguments$)",
                new ExpressionPlaceholder("container", "Castle.Windsor.IWindsorContainer"),
                new ArgumentPlaceholder("arguments", -1, -1)); // any number of arguments

        private readonly IRegistrationPattern componentForPattern = new ComponentForGeneric();
        private readonly IRegistrationPattern implementedByPattern = new ImplementedByGeneric();

        public ContainerRegisterPattern()
            : base(pattern)
        {
        }

        public override IComponentRegistrationCreator CreateComponentRegistrationCreator()
        {
            return new CompositeCreator(this);
        }

        private class CompositeCreator : IComponentRegistrationCreator
        {
            private readonly IRegistrationPattern componentForPattern;
            private readonly IRegistrationPattern implementedByPattern;

            public CompositeCreator(ContainerRegisterPattern parent)
            {
                componentForPattern = parent.componentForPattern;
                implementedByPattern = parent.implementedByPattern;
            }

            public IEnumerable<IComponentRegistration> CreateRegistrations(params IStructuralMatchResult[] matchResults)
            {
                return from match in matchResults
                       from argument in match.GetMatchedElementList("arguments").OfType<ICSharpArgument>()
                       from registration in MatchElement(argument.Value)
                       select registration;
            }

            private IEnumerable<IComponentRegistration> MatchElement(ICSharpExpression element)
            {
                IStructuralMatcher componetForMatcher = componentForPattern.CreateMatcher();
                IStructuralMatcher implementedByMatcher = implementedByPattern.CreateMatcher();
                
                IStructuralMatchResult implByResult = implementedByMatcher.Match(element);
                if (implByResult.Matched)
                {
                    IComponentRegistrationCreator creator = implementedByPattern.CreateComponentRegistrationCreator();
                    foreach (var registration in creator.CreateRegistrations(implByResult))
                    {
                        yield return registration;
                    }
                }
                else
                {
                    IStructuralMatchResult componentForResult = componetForMatcher.Match(element);
                    if (componentForResult.Matched)
                    {
                        IComponentRegistrationCreator creator = componentForPattern.CreateComponentRegistrationCreator();
                        foreach (var registration in creator.CreateRegistrations(componentForResult))
                        {
                            yield return registration;
                        }
                    }
                }
            }
        }

        private class ImplementedByGeneric : ManualRegistrationBase
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
    }
}
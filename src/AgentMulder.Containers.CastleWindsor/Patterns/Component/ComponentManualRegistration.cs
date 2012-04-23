using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using AgentMulder.ReSharper.Domain.Registrations;
using AgentMulder.ReSharper.Domain.Search;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.Containers.CastleWindsor.Patterns.Component
{
    [PartNotDiscoverable]
    internal sealed class ComponentManualRegistration : RegistrationBase
    {
        private static readonly RegistrationBase componentForPattern = new ComponentForGeneric();
        private static readonly RegistrationBase implementedByPattern = new ImplementedByGeneric();

        public ComponentManualRegistration()
            : base(componentForPattern.Pattern)
        {
        }

        public override IComponentRegistrationCreator CreateComponentRegistrationCreator()
        {
            return new CompositeCreator(componentForPattern, implementedByPattern);
        }

        private class CompositeCreator : IComponentRegistrationCreator
        {
            private readonly IRegistrationPattern componentFor;
            private readonly IRegistrationPattern implBy;

            public CompositeCreator(IRegistrationPattern componentFor, IRegistrationPattern implBy)
            {
                this.componentFor = componentFor;
                this.implBy = implBy;
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
                IStructuralMatcher componetForMatcher = componentFor.CreateMatcher();
                IStructuralMatcher implementedByMatcher = implBy.CreateMatcher();

                IStructuralMatchResult implByResult = implementedByMatcher.Match(element);
                if (implByResult.Matched)
                {
                    IComponentRegistrationCreator creator = implBy.CreateComponentRegistrationCreator();
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
                        IComponentRegistrationCreator creator = componentFor.CreateComponentRegistrationCreator();
                        foreach (var registration in creator.CreateRegistrations(componentForResult))
                        {
                            yield return registration;
                        }
                    }
                }
            }
        }
    }
}
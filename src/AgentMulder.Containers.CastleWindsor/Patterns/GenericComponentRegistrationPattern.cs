using System.Collections.Generic;
using System.Linq;
using AgentMulder.ReSharper.Domain.Registrations;
using AgentMulder.ReSharper.Domain.Search;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.Containers.CastleWindsor.Patterns
{
    public class GenericComponentRegistrationPattern : IComponentRegistrationPattern
    {
        private readonly IStructuralSearchPattern pattern;

        public GenericComponentRegistrationPattern()
        {
            pattern = new CSharpStructuralSearchPattern("$component$.For<$service$>().ImplementedBy<$impl$>()",
                new ExpressionPlaceholder("component", "Castle.MicroKernel.Registration.Component"),
                new TypePlaceholder("service"),
                new TypePlaceholder("impl"));   
        }

        public IStructuralMatcher CreateMatcher()
        {
            return pattern.CreateMatcher();
        }

        public IComponentRegistrationCreator CreateComponentRegistrationCreator()
        {
            return new GenericComponentRegistrationsCreator();
        }

        private sealed class GenericComponentRegistrationsCreator : IComponentRegistrationCreator
        {
            public IEnumerable<IComponentRegistration> CreateRegistrations(params IStructuralMatchResult[] matchResults)
            {
                return (from match in matchResults
                        let matchedType = match.GetMatchedType("impl") as IDeclaredType
                        where matchedType != null
                        select new ComponentRegistration(match.GetDocumentRange(), matchedType.GetTypeElement()));
            }
        }
    }
}
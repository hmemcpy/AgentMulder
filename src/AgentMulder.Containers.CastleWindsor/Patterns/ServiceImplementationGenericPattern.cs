using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AgentMulder.ReSharper.Domain.Registrations;
using AgentMulder.ReSharper.Domain.Search;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.Containers.CastleWindsor.Patterns
{
    public class ServiceImplementationGenericPattern : IComponentRegistrationPattern
    {
        private readonly IStructuralSearchPattern pattern;

        public ServiceImplementationGenericPattern()
        {
            pattern = new CSharpStructuralSearchPattern("$component$.For<$service$>().ImplementedBy<$impl$>()",
                new ExpressionPlaceholder("component", "Castle.MicroKernel.Registration.Component"),
                new TypePlaceholder("service"),
                new TypePlaceholder("impl"));

            Debug.Assert(pattern.Check() == null);
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
                foreach (IStructuralMatchResult match in matchResults)
                {
                    var matchedType = match.GetMatchedType("impl") as IDeclaredType;
                    if (matchedType != null)
                    {
                        yield return new ComponentRegistration(match.GetDocumentRange(), matchedType.GetTypeElement());
                    }
                }
            }
        }
    }

    public class ServiceRegistrationGenericPattern : IComponentRegistrationPattern
    {
        private readonly IStructuralSearchPattern pattern;

        public ServiceRegistrationGenericPattern()
        {
            pattern = new CSharpStructuralSearchPattern("$component$.For<$service$>()",
                new ExpressionPlaceholder("component", "Castle.MicroKernel.Registration.Component"),
                new TypePlaceholder("service"));

            Debug.Assert(pattern.Check() == null);
        }

        public IStructuralMatcher CreateMatcher()
        {
            return pattern.CreateMatcher();
        }

        public IComponentRegistrationCreator CreateComponentRegistrationCreator()
        {
            return new ServiceRegistrationCreator();
        }

        private sealed class ServiceRegistrationCreator : IComponentRegistrationCreator
        {
            public IEnumerable<IComponentRegistration> CreateRegistrations(params IStructuralMatchResult[] matchResults)
            {
                foreach (IStructuralMatchResult match in matchResults)
                {
                    var matchedType = match.GetMatchedType("service") as IDeclaredType;
                    if (matchedType != null)
                    {
                        yield return new ComponentRegistration(match.GetDocumentRange(), matchedType.GetTypeElement());
                    }
                }
            }
        }
    }
}
using System.Collections.Generic;
using AgentMulder.ReSharper.Domain.Registrations;
using AgentMulder.ReSharper.Domain.Search;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.Containers.CastleWindsor.Patterns
{
    internal class ServiceWithImplementationRegistrationPattern : ComponentRegistrationPatternBase
    {
        private static readonly IStructuralSearchPattern pattern = 
            new CSharpStructuralSearchPattern("$component$.For<$service$>().ImplementedBy<$impl$>()",
                                              new ExpressionPlaceholder("component", "Castle.MicroKernel.Registration.Component"),
                                              new TypePlaceholder("service"),
                                              new TypePlaceholder("impl"));


        public ServiceWithImplementationRegistrationPattern()
            : base(pattern)
        {
        }

        public override IComponentRegistrationCreator CreateComponentRegistrationCreator()
        {
            return new ServiceWithImplementationCreator();
        }

        private sealed class ServiceWithImplementationCreator : IComponentRegistrationCreator
        {
            public IEnumerable<IComponentRegistration> CreateRegistrations(params IStructuralMatchResult[] matchResults)
            {
                foreach (IStructuralMatchResult match in matchResults)
                {
                    var matchedType = match.GetMatchedType("impl") as IDeclaredType;
                    if (matchedType != null)
                    {
                        ITypeElement typeElement = matchedType.GetTypeElement(match.MatchedElement.GetPsiModule());
                        yield return new ComponentRegistration(match.GetDocumentRange(), typeElement);
                    }
                }
            }
        }

    }
}
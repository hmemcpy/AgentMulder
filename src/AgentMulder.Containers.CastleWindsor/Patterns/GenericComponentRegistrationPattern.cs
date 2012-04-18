using System.Collections.Generic;
using System.Linq;
using AgentMulder.ReSharper.Domain;
using AgentMulder.ReSharper.Domain.Registrations;
using AgentMulder.ReSharper.Domain.Search;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

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

        public IStructuralSearchPattern Pattern
        {
            get { return pattern; }
        }

        public IEnumerable<IComponentRegistration> CreateRegistrations(IPatternSearcher patternSearcher)
        {
            if (pattern.Check() != null)
            {
                yield break;
            }

            IEnumerable<IStructuralMatchResult> results = patternSearcher.Search(this);

            if (results == null)
            {
                yield break;
            }

            foreach (IStructuralMatchResult match in results)
            {
                IType matchedType = match.GetMatchedType("impl");
                var declaredType = matchedType as IDeclaredType;
                if (declaredType != null)
                {
                    yield return new ConcreteRegistration(declaredType.GetTypeElement());
                }
            }
        }
    }
}
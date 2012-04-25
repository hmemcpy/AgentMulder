using AgentMulder.ReSharper.Domain.Search;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.WithService
{
    public abstract class WithServiceRegistrationBase : RegistrationBase
    {
        protected WithServiceRegistrationBase(IStructuralSearchPattern pattern)
            : base(pattern)
        {
        }
    }
}
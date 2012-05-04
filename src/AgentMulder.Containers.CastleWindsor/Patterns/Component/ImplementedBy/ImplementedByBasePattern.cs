using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.Containers.CastleWindsor.Patterns.Component.ImplementedBy
{
    public abstract class ImplementedByBasePattern : ComponentRegistrationBasePattern
    {
        protected ImplementedByBasePattern(IStructuralSearchPattern pattern, string elementName)
            : base(pattern, elementName)
        {
        }
    }
}
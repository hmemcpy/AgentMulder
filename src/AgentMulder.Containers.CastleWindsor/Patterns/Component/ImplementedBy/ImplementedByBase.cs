using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.Containers.CastleWindsor.Patterns.Component.ImplementedBy
{
    internal abstract class ImplementedByBase : ComponentRegistrationBase
    {
        protected ImplementedByBase(IStructuralSearchPattern pattern, string elementName)
            : base(pattern, elementName)
        {
        }
    }
}
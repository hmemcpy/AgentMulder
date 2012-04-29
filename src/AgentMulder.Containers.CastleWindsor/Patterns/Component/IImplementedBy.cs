using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.Containers.CastleWindsor.Patterns.Component
{
    internal abstract class ImplementedByBase : ComponentRegistrationBase
    {
        protected ImplementedByBase(IStructuralSearchPattern pattern, string elementName)
            : base(pattern, elementName)
        {
        }
    }
}
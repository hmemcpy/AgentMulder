using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.ReSharper.Domain.Patterns
{
    public abstract class ComponentImplementationPatternBase : ComponentRegistrationPatternBase
    {
        protected ComponentImplementationPatternBase(IStructuralSearchPattern pattern, string elementName)
            : base(pattern, elementName)
        {
        }
    }
}
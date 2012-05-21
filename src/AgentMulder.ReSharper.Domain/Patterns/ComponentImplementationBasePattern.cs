using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.ReSharper.Domain.Patterns
{
    public abstract class ComponentImplementationBasePattern : ComponentRegistrationBasePattern
    {
        protected ComponentImplementationBasePattern(IStructuralSearchPattern pattern, string elementName)
            : base(pattern, elementName)
        {
        }
    }
}
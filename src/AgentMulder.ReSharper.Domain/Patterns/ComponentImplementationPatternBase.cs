#if SDK90
using JetBrains.ReSharper.Feature.Services.StructuralSearch;
#else
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
#endif

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
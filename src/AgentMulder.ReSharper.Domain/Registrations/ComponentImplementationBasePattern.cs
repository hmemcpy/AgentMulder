using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.ReSharper.Domain.Registrations
{
    public abstract class ComponentImplementationBasePattern : ComponentRegistrationBasePattern
    {
        protected ComponentImplementationBasePattern(IStructuralSearchPattern pattern, string elementName)
            : base(pattern, elementName)
        {
        }
    }
}
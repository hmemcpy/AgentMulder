using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.ReSharper.Domain.Patterns
{
    public interface IStructuralPatternHolder
    {
        IStructuralSearchPattern Pattern { get; }
        IStructuralMatcher Matcher { get; }
    }
}
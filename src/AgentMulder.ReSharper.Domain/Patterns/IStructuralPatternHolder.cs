using JetBrains.ReSharper.Feature.Services.StructuralSearch;
using JetBrains.ReSharper.Psi;

namespace AgentMulder.ReSharper.Domain.Patterns
{
    public interface IStructuralPatternHolder
    {
        IStructuralSearchPattern Pattern { get; }
        IStructuralMatcher Matcher { get; }
        PsiLanguageType Language { get; }
    }
}
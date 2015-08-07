using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Feature.Services.StructuralSearch;

namespace AgentMulder.ReSharper.Domain.Patterns
{
    public interface IStructuralPatternHolder
    {
        IStructuralSearchPattern Pattern { get; }
        IStructuralMatcher Matcher { get; }
        PsiLanguageType Language { get; }
    }
}
using JetBrains.ReSharper.Psi;
#if SDK90
using JetBrains.ReSharper.Feature.Services.StructuralSearch;
#else
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
#endif

namespace AgentMulder.ReSharper.Domain.Patterns
{
    public interface IStructuralPatternHolder
    {
        IStructuralSearchPattern Pattern { get; }
        IStructuralMatcher Matcher { get; }
        PsiLanguageType Language { get; }
    }
}
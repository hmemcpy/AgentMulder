using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.ReSharper.Plugin.Components
{
    public interface ITypeUsageManager
    {
        void MarkTypeAsUsed(ITypeDeclaration declaration);
    }
}
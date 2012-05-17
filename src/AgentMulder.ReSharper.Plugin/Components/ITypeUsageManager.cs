using JetBrains.ReSharper.Psi;

namespace AgentMulder.ReSharper.Plugin.Components
{
    public interface ITypeUsageManager
    {
        void MarkTypeAsUsed(ITypeElement typeElement);
    }
}
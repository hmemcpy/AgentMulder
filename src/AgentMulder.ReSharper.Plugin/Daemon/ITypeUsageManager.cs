using JetBrains.ReSharper.Psi;

namespace AgentMulder.ReSharper.Plugin.Daemon
{
    public interface ITypeUsageManager
    {
        void MarkTypeAsUsed(ITypeElement typeElement);
    }
}
using JetBrains.ReSharper.Psi;

namespace AgentMulder.ReSharper.Plugin.Daemon
{
    public interface ITypeUsageManager
    {
        void MarkTypeUsed(ITypeElement typeElement);
    }
}
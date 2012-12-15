using JetBrains.Application.Progress;
using JetBrains.ReSharper.Psi.Caches;

namespace AgentMulder.ReSharper.Plugin.Components
{
    // for 7.1
    public partial class PatternManagerCache
    {
        object ICache.Load(IProgressIndicator progress, bool enablePersistence)
        {
            return null;
        }

        void ICache.Save(IProgressIndicator progress, bool enablePersistence)
        {
        }
    }
}
using System;
using JetBrains.Application.Progress;
using JetBrains.ReSharper.Psi.Caches;
using JetBrains.ReSharper.Psi.Util.Caches;

namespace AgentMulder.ReSharper.Plugin.Components
{
    public partial class PatternManagerCache
    {
        object ICache.Load(IProgressIndicator progress, bool enablePersistence, PersistentIdIndex persistentIdIndex)
        {
            return null;
        }

        void ICache.Save(IProgressIndicator progress, bool enablePersistence, PersistentIdIndex persistentIdIndex)
        {
        }
    }
}
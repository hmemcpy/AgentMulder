using JetBrains.Application.Progress;
using JetBrains.DocumentManagers.impl;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Caches;

namespace AgentMulder.ReSharper.Plugin.Components
{
    public partial class PatternManagerCache
    {
        object ICache.Load(IProgressIndicator progress, bool enablePersistence)
        {
            return null;
        }

        void ICache.Save(IProgressIndicator progress, bool enablePersistence)
        {
        }

        public void Drop(IPsiSourceFile sourceFile)
        {
            
        }

        public void OnDocumentChange(IPsiSourceFile sourceFile, ProjectFileDocumentCopyChange change)
        {
            
        }
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using JetBrains.DataFlow;
using JetBrains.DocumentManagers.impl;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Caches;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.Impl.Caches2;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Util;

namespace AgentMulder.ReSharper.Plugin.Components
{
    [SolutionComponent]
    public partial class PatternManagerCache : IPatternManager, ICache
    {
        private readonly object lockObject = new object();
        private readonly PsiProjectFileTypeCoordinator projectFileTypeCoordinator;
        private readonly SolutionAnalyzer solutionAnalyzer;
        private readonly JetHashSet<IPsiSourceFile> dirtyFiles = new JetHashSet<IPsiSourceFile>();
        private readonly OneToListMap<IPsiSourceFile, RegistrationInfo> registrationsMap = new OneToListMap<IPsiSourceFile, RegistrationInfo>();

        public PatternManagerCache(Lifetime lifetime, CacheManagerEx cacheManager, PsiProjectFileTypeCoordinator projectFileTypeCoordinator, SolutionAnalyzer solutionAnalyzer)
        {
            this.projectFileTypeCoordinator = projectFileTypeCoordinator;
            this.solutionAnalyzer = solutionAnalyzer;

            lifetime.AddBracket(() => cacheManager.RegisterCache(this), () => cacheManager.UnregisterCache(this));
        }

        public IEnumerable<RegistrationInfo> GetRegistrationsForFile(IPsiSourceFile sourceFile)
        {
            if (!((ICache)this).UpToDate(sourceFile))
            {
                Merge(sourceFile, ProcessSourceFile(sourceFile));
            }

            lock (lockObject)
            {
                return registrationsMap.Values;
            }
        }

        object ICache.Build(IPsiAssembly assembly)
        {
            return null;
        }

        object ICache.Build(IPsiSourceFile sourceFile, bool isStartup)
        {
            return ProcessSourceFile(sourceFile);
        }

        private IEnumerable<RegistrationInfo> ProcessSourceFile(IPsiSourceFile sourceFile)
        {
            return solutionAnalyzer.Analyze(sourceFile);
        }

        public bool HasDirtyFiles
        {
            get { return Enumerable.Any(dirtyFiles); }
        }

        void ICache.MarkAsDirty(IPsiSourceFile sourceFile)
        {
            lock (lockObject)
            {
                dirtyFiles.Add(sourceFile);                
            }
        }

        void ICache.Merge(IPsiAssembly assembly, object part)
        {
        }

        void ICache.Merge(IPsiSourceFile sourceFile, object builtPart)
        {
            if (builtPart == null)
            {
                return;
            }

            Merge(sourceFile, (IEnumerable<RegistrationInfo>)builtPart);
        }

        private void Merge(IPsiSourceFile sourceFile, IEnumerable<RegistrationInfo> items)
        {
            lock (lockObject)
            {
                registrationsMap.RemoveKey(sourceFile);
                registrationsMap.AddValueRange(sourceFile, items);

                dirtyFiles.Remove(sourceFile);
            }
        }

        void ICache.MergeLoaded(object data)
        {
        }

        void ICache.OnAssemblyRemoved(IPsiAssembly assembly)
        {
        }

        void ICache.OnDocumentChange(ProjectFileDocumentCopyChange change)
        {
            foreach (IPsiSourceFile sourceFile in change.ProjectFile.ToSourceFiles())
            {
                ((ICache)this).MarkAsDirty(sourceFile);
            }
        }

        void ICache.OnFileRemoved(IPsiSourceFile sourceFile)
        {
            RemoveFileItems(sourceFile);
        }

        private void RemoveFileItems(IPsiSourceFile sourceFile)
        {
            lock (lockObject)
            {
                if (!registrationsMap.ContainsKey(sourceFile))
                {
                    return;
                }

                registrationsMap.RemoveKey(sourceFile);
            }
        }

        IEnumerable<IPsiSourceFile> ICache.OnProjectModelChange(ProjectModelChange change)
        {
            return EmptyList<IPsiSourceFile>.InstanceList;
        }

        void ICache.OnPsiChange(ITreeNode elementContainingChanges, PsiChangedElementType type)
        {
        }

        IEnumerable<IPsiSourceFile> ICache.OnPsiModulePropertiesChange(IPsiModule module)
        {
            return EmptyList<IPsiSourceFile>.InstanceList;
        }

        void ICache.OnSandBoxCreated(SandBox sandBox)
        {
        }

        void ICache.OnSandBoxPsiChange(ITreeNode elementContainingChanges)
        {
        }

        void ICache.Release()
        {
        }

        void ICache.SyncUpdate(bool underTransaction)
        {
        }

        bool ICache.UpToDate(IPsiSourceFile sourceFile)
        {
            lock (lockObject)
            {
                if (dirtyFiles.Contains(sourceFile))
                {
                    return false;
                }

                if (!registrationsMap.ContainsKey(sourceFile))
                {
                    return false;
                }
            }

            return !ShouldBeProcessed(sourceFile);
        }

        private bool ShouldBeProcessed(IPsiSourceFile sourceFile)
        {
            if (!sourceFile.Properties.ShouldBuildPsi)
            {
                return false;
            }

            ProjectFileType languageType = sourceFile.LanguageType;
            return !languageType.IsNullOrUnknown() && projectFileTypeCoordinator.TryGetService(languageType) != null;
        }
    }
}
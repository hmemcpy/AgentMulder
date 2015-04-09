using JetBrains.Application.Progress;
using JetBrains.DataFlow;
using JetBrains.DocumentManagers.impl;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Caches;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Util;
using System;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AgentMulder.ReSharper.Plugin.Components
{
    [SolutionComponent]
    public class PatternManagerCache : IPatternManager, ICache
    {
        private readonly object lockObject = new object();
        private readonly JetHashSet<IPsiSourceFile> dirtyFiles = new JetHashSet<IPsiSourceFile>();
        private readonly PsiProjectFileTypeCoordinator projectFileTypeCoordinator;
        private readonly SolutionAnalyzer solutionAnalyzer;

        private readonly OneToListMap<IPsiSourceFile, RegistrationInfo> registrationsMap =
            new OneToListMap<IPsiSourceFile, RegistrationInfo>();

        public PatternManagerCache(SolutionAnalyzer solutionAnalyzer, PsiProjectFileTypeCoordinator projectFileTypeCoordinator)
        {
            this.projectFileTypeCoordinator = projectFileTypeCoordinator;
            this.solutionAnalyzer = solutionAnalyzer;
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

        void ICache.Save(IProgressIndicator progress, bool enablePersistence)
        {
        }

        void ICache.MarkAsDirty(IPsiSourceFile sourceFile)
        {
            MarkAsDirty(sourceFile);
        }

        private void MarkAsDirty(IPsiSourceFile sourceFile)
        {
            lock (lockObject)
            {
                dirtyFiles.Add(sourceFile);
            }
        }

        void ICache.Merge(IPsiSourceFile sourceFile, object builtPart)
        {
            if (builtPart == null)
            {
                return;
            }

            Merge(sourceFile, (IEnumerable<RegistrationInfo>)builtPart);
        }

        void ICache.Drop(IPsiSourceFile sourceFile)
        {
            RemoveFileItems(sourceFile);
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

        object ICache.Load(IProgressIndicator progress, bool enablePersistence)
        {
            return null;
        }

        void ICache.MergeLoaded(object data)
        {
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

        void ICache.OnPsiChange(ITreeNode elementContainingChanges, PsiChangedElementType type)
        {
        }

        void ICache.OnDocumentChange(IPsiSourceFile sourceFile, ProjectFileDocumentCopyChange change)
        {
            MarkAsDirty(sourceFile);

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
using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Application;
using JetBrains.Application.Progress;
using JetBrains.DataFlow;
using JetBrains.DocumentManagers.impl;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Caches;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.Impl.Caches2;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Util;
#if !SDK70
using JetBrains.ReSharper.Psi.Util.Caches;
#endif

namespace AgentMulder.ReSharper.Plugin.Components
{
    [SolutionComponent]
    public class PatternManager : IPatternManager
    {
        private readonly SolutionAnalyzer analyzer;

        public PatternManager(SolutionAnalyzer analyzer)
        {
            this.analyzer = analyzer;
        }

        public IEnumerable<RegistrationInfo> GetRegistrationsForFile(IPsiSourceFile psiSourceFile)
        {
            return analyzer.Analyze();
        }
    }

    //[SolutionComponent]
//    public sealed class PatternManager : IPatternManager, ICache, IAsyncCommitClient
//    {
//        private readonly object syncRoot = new object();
//        private readonly HashSet<IPsiSourceFile> dirtyFiles = new HashSet<IPsiSourceFile>();
//        private readonly Lifetime lifetime;
//        private readonly IShellLocks locks;
//        private readonly ISolution solution;
//        private readonly CacheManagerEx cacheManager;
//        private readonly SolutionAnalyzer solutionAnalyzer;
//        private readonly AsyncCommitService asyncCommitter;
//        private readonly PsiProjectFileTypeCoordinator projectFileTypeCoordinator;
//        private volatile int versionStamp;
//        private readonly OneToListMap<IPsiSourceFile, RegistrationInfo> registrationsLookup = new OneToListMap<IPsiSourceFile, RegistrationInfo>();

//        public PatternManager(Lifetime lifetime, IShellLocks locks, ISolution solution, CacheManagerEx cacheManager, SolutionAnalyzer solutionAnalyzer,
//                                   AsyncCommitService asyncCommitter,
//                                   PsiProjectFileTypeCoordinator projectFileTypeCoordinator)
//        {
//            this.lifetime = lifetime;
//            this.locks = locks;
//            this.solution = solution;
//            this.cacheManager = cacheManager;
//            this.solutionAnalyzer = solutionAnalyzer;
//            this.asyncCommitter = asyncCommitter;
//            this.projectFileTypeCoordinator = projectFileTypeCoordinator;

//            lifetime.AddBracket(() => cacheManager.RegisterCache(this), () => cacheManager.UnregisterCache(this));
//        }

//        private void Initialize()
//        {
//            //var persistentMap = persistentIndexManager.GetPersistentMap(
//            //    PersistentCachesUniqueIds.TodoManager,
//            //    new UnsafeFilteredCollectionMarshaller<RegistrationInfo, IList<RegistrationInfo>>(
//            //        new PatternItemMarshaller(persistentIndexManager, this),
//            //        _ => new List<RegistrationInfo>(_),
//            //        UnsafeMarshallers.NotNullPredicate));

//            //fileToItemsPersistent = CacheBuilder<IPsiSourceFile, IList<RegistrationInfo>>
//            //    .NewInstance()
//            //    .Associativity(0)
//            //    .CacheLoad(CacheLoad.Lazy)
//            //    .WriteMiss(WriteMiss.WriteAllocate)
//            //    .WritePolicy(new WritePolicy(100))
//            //    .Capacity(Math.Max(persistentIndexManager.Count * 3 / 2, 1000))
//            //    .Build(persistentIndexManager.GetPersistentDb(), persistentMap);

//            IEnumerable<RegistrationInfo> registrationInfos = solutionAnalyzer.Analyze();
//            Assertion.Assert(!registrationInfos.IsEmpty(), "No registrations were picked up!");
//        }

//        private void RescanSolution()
//        {
//            locks.ExecuteOrQueueReadLock(lifetime, "AgentMulder.RescanAllSolution", () =>
//            {
//                lock (syncRoot)
//                {
//                    var visitor = new CacheInvalidatingVisitor(this);
//                    solution.Accept(visitor);
//                    cacheManager.AsyncUpdateFiles(visitor.DirtyFiles);
//                }
//                cacheManager.EnqueueJob(new DelegateJob(OnUpdated, "AgentMulder: Cache was updated"));
//                OnUpdated();
//            });
//        }

//        private sealed class CacheInvalidatingVisitor : RecursiveProjectVisitor
//        {
//            private readonly PatternManager myManager;

//            public List<IPsiSourceFile> DirtyFiles { get; private set; }

//            public CacheInvalidatingVisitor(PatternManager manager)
//            {
//                this.myManager = manager;
//                this.DirtyFiles = new List<IPsiSourceFile>();
//            }

//            public override void VisitProjectFile(IProjectFile projectFile)
//            {
//                IPsiSourceFile sourceFile = projectFile.ToSourceFile();
//                if (sourceFile == null || !this.myManager.ShouldBeProcessed(sourceFile))
//                    return;
//                this.myManager.MarkAsDirty(sourceFile);
//                this.DirtyFiles.Add(sourceFile);
//            }
//        }

//        public IEnumerable<RegistrationInfo> GetRegistrationsForFile(IPsiSourceFile sourceFile)
//        {
//            //if (!UpToDate(sourceFile))
//            //{
//            //    Merge(sourceFile, (IList<RegistrationInfo>)ProcessSourceFile(sourceFile));
//            //    OnUpdated();
//            //}

//            lock (syncRoot)
//            {
//                return registrationsLookup[sourceFile];
//            }
//        }
        
//        internal void OnUpdated()
//        {
//            if (lifetime.IsTerminated)
//            {
//                return;
//            }
//            locks.ExecuteOrQueueReadLock(lifetime, "PatternManager::OnUpdated", FireOnUpdated);
//        }

//        private void FireOnUpdated()
//        {
//            locks.Dispatcher.AssertAccess();
//        }

//        private IEnumerable<RegistrationInfo> ProcessSourceFile(IPsiSourceFile sourceFile)
//        {
//            //{
//            //    if (sourceFile.LanguageType.IsNullOrUnknown() || sourceFile.Properties.IsNonUserFile)
//            //        return EmptyList<RegistrationInfo>.InstanceList;

//            //    List<RegistrationInfo> matchingRegistrations;
//            //    registrationsLookup.TryGetValue(sourceFile, out matchingRegistrations);

//            //    return matchingRegistrations ?? EmptyList<RegistrationInfo>.InstanceList;

//            if (sourceFile.LanguageType.IsNullOrUnknown() || sourceFile.Properties.IsNonUserFile)
//                return EmptyList<RegistrationInfo>.InstanceList;

//            return FindRegistrations(sourceFile);
//        }

//        private IEnumerable<RegistrationInfo> FindRegistrations(IPsiSourceFile sourceFile)
//        {
//            return solutionAnalyzer.Analyze(sourceFile);
//        }

//        private void Merge(IPsiSourceFile sourceFile, IList<RegistrationInfo> items)
//        {
//            lock (syncRoot)
//            {
//                int changes = 0;
//                IList<RegistrationInfo> patterns = registrationsLookup[sourceFile];
//                if (patterns.Count != items.Count)
//                {
//                    patterns = items;
//                    ++changes;
//                }
//                else
//                {
//                    changes = patterns.Where((pattern, i) => !pattern.Equals(items[i])).Count();
//                }

//                registrationsLookup[sourceFile].AddRange(patterns);

//                if (changes > 0)
//                    OnChange();

//                dirtyFiles.Remove(sourceFile);
//            }
//        }

//        private void OnChange()
//        {
//            lock (syncRoot)
//            {
//                ++versionStamp;
//            }
//        }

//        object ICache.Build(IPsiAssembly assembly)
//        {
//            return null;
//        }

//        object ICache.Build(IPsiSourceFile sourceFile, bool isStartup)
//        {
//            if (projectFileTypeCoordinator.TryGetService(sourceFile.LanguageType) == null)
//            {
//                return null;
//            }

//            return ProcessSourceFile(sourceFile);
//        }

//        bool ICache.HasDirtyFiles
//        {
//            get
//            {
//                lock (syncRoot)
//                {
//                    return dirtyFiles.Any();
//                }
//            }
//        }


//#if SDK70
//        void ICache.Save(IProgressIndicator progress, bool enablePersistence)
//        {
//        }

//        object ICache.Load(IProgressIndicator progress, bool enablePersistence)
//        {
//            Initialize();
//            OnChange();
//            OnUpdated();
//            return null;
//        }
//#else
//        void ICache.Save(IProgressIndicator progress, bool enablePersistence, PersistentIdIndex persistentIdIndex)
//        {
//            throw new NotImplementedException();
//        }

//        object ICache.Load(IProgressIndicator progress, bool enablePersistence, PersistentIdIndex persistentIdIndex)
//        {
//            Initialize();
//            OnChange();
//            OnUpdated();
//            return null;
//        }
//#endif

//        public void MarkAsDirty(IPsiSourceFile sourceFile)
//        {
//            lock (syncRoot)
//            {
//                dirtyFiles.Add(sourceFile);
//            }
//        }

//        void ICache.Merge(IPsiAssembly assembly, object part)
//        {
//        }

//        void ICache.Merge(IPsiSourceFile sourceFile, object builtPart)
//        {
//            if (builtPart == null)
//            {
//                return;
//            }

//            Merge(sourceFile, (IList<RegistrationInfo>)builtPart);
//        }

//        void ICache.MergeLoaded(object data)
//        {
//        }

//        void ICache.OnAssemblyRemoved(IPsiAssembly assembly)
//        {
//        }

//        void ICache.OnDocumentChange(ProjectFileDocumentCopyChange change)
//        {
//            foreach (IPsiSourceFile sourceFile in change.ProjectFile.ToSourceFiles())
//            {
//                MarkAsDirty(sourceFile);
//            }
//        }

//        void ICache.OnFileRemoved(IPsiSourceFile sourceFile)
//        {
//            RemoveFileItems(sourceFile);
//        }

//        private void RemoveFileItems(IPsiSourceFile sourceFile)
//        {
//            lock (syncRoot)
//            {
//                if (!registrationsLookup.RemoveKey(sourceFile))
//                    return;

//                OnChange();
//            }
//        }

//        IEnumerable<IPsiSourceFile> ICache.OnProjectModelChange(ProjectModelChange change)
//        {
//            return EmptyList<IPsiSourceFile>.InstanceList;
//        }

//        void ICache.OnPsiChange(ITreeNode elementContainingChanges, PsiChangedElementType type)
//        {
//        }

//        IEnumerable<IPsiSourceFile> ICache.OnPsiModulePropertiesChange(IPsiModule module)
//        {
//            return EmptyList<IPsiSourceFile>.InstanceList;
//        }

//        void ICache.OnSandBoxCreated(SandBox sandBox)
//        {
//        }

//        void ICache.OnSandBoxPsiChange(ITreeNode elementContainingChanges)
//        {
//        }

//        void ICache.Release()
//        {
//        }

//        void ICache.SyncUpdate(bool underTransaction)
//        {
//            if (underTransaction)
//                return;
//            lock (syncRoot)
//            {
//                if (dirtyFiles.IsEmpty())
//                    return;
//            }

//            asyncCommitter.RequestCommit(this);
//        }

//        public bool UpToDate(IPsiSourceFile sourceFile)
//        {
//            return true;
//            lock (syncRoot)
//            {
//                if (dirtyFiles.Contains(sourceFile))
//                {
//                    return false;
//                }

//                return !ShouldBeProcessed(sourceFile) || registrationsLookup.ContainsKey(sourceFile);
//            }
//        }

//        private bool ShouldBeProcessed(IPsiSourceFile sourceFile)
//        {
//            return false;
//            if (!sourceFile.Properties.ShouldBuildPsi)
//            {
//                return false;
//            }

//            ProjectFileType languageType = sourceFile.LanguageType;

//            return !languageType.IsNullOrUnknown() && projectFileTypeCoordinator.TryGetService(languageType) != null;
//        }

//        Action IAsyncCommitClient.BeforeCommit()
//        {
//            lock (syncRoot)
//            {
//                if (dirtyFiles.Count == 0)
//                {
//                    return null;
//                }
//            }
//#if SDK70
//            return ((InterruptableReadActivity)new InterruptableReadActivityThe(locks, () => lifetime.IsTerminated)
//#else
//            return ((InterruptableReadActivity)new InterruptableReadActivityThe(() => lifetime.IsTerminated)
//#endif
//            {
//                Name = "AgentMulderBackgroundUpdate",
//                FuncRun = (Action)(RescanDirtyFiles),
//                FuncCancelled = (Action)(OnUpdated),
//                FuncCompleted = (Action)(OnUpdated)
//            }).DoStart;
//        }

//        private void RescanDirtyFiles()
//        {
//            HashSet<IPsiSourceFile> hashSet;
//            lock (syncRoot)
//            {
//                hashSet = new HashSet<IPsiSourceFile>(dirtyFiles);
//                dirtyFiles.Clear();
//            }
//            try
//            {
//                foreach (IPsiSourceFile sourceFile in hashSet.ToArray())
//                {
//                    InterruptableActivityCookie.CheckAndThrow();
//                    if (sourceFile.IsValid())
//                    {
//                        Merge(sourceFile, (IList<RegistrationInfo>)ProcessSourceFile(sourceFile));
//                    }
//                    hashSet.Remove(sourceFile);
//                }
//            }
//            finally
//            {
//                if (hashSet.Any())
//                {
//                    lock (syncRoot)
//                    {
//                        dirtyFiles.UnionWith(hashSet);
//                    }
//                }
//            }
//        }

//        void IAsyncCommitClient.OnInterrupt()
//        {
//            lock (syncRoot)
//            {
//                if (dirtyFiles.Count == 0)
//                {
//                    return;
//                }

//                asyncCommitter.RequestCommit(this);
//            }
//        }
//    }

    public interface IPatternManager
    {
        IEnumerable<RegistrationInfo> GetRegistrationsForFile(IPsiSourceFile psiSourceFile);
    }
}
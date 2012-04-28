using System;
using System.IO;
using JetBrains.Application.Settings;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Daemon.UsageChecking;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Search;

namespace AgentMulder.ReSharper.Plugin.Daemon
{
    [DaemonStage(StagesBefore = new[] { typeof(LanguageSpecificDaemonStage) })]
    public class ContainerAnalysisDaemonStage : IDaemonStage
    {
        private readonly SearchDomainFactory searchDomainFactory;

        public ContainerAnalysisDaemonStage(SearchDomainFactory searchDomainFactory)
        {
            this.searchDomainFactory = searchDomainFactory;
        }

        public IDaemonStageProcess CreateProcess(IDaemonProcess process, IContextBoundSettingsStore settings, DaemonProcessKind processKind)
        {
            var usagesStageProcess = process.GetStageProcess<CollectUsagesStageProcess>();

            string containerDirectory = GetContainersDirectory();

            return new ContainerAnalysisStageProcess(process,
                                                     new TypeUsageManager(usagesStageProcess),
                                                     searchDomainFactory,
                                                     containerDirectory);
        }

        private static string GetContainersDirectory()
        {
            // todo unfortunately, in tests this returns a different path, breaking MEF
            string rootDirectory = Path.GetDirectoryName(typeof(ContainerAnalysisStageProcess).Assembly.Location);
            string containerDirectory = Path.Combine(rootDirectory, "Containers");
            return containerDirectory;
        }

        public ErrorStripeRequest NeedsErrorStripe(IPsiSourceFile sourceFile, IContextBoundSettingsStore settingsStore)
        {
            return ErrorStripeRequest.NONE;
        }
    }
}
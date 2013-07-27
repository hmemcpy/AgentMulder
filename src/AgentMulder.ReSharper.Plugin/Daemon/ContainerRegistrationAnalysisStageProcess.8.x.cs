using System;
using System.Collections.Generic;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.ReSharper.Plugin.Daemon
{
    // ReSharper 8.x
    public partial class ContainerRegistrationAnalysisStageProcess
    {
        private IEnumerable<IFile> EnumeratePsiFiles()
        {
            return DaemonProcess.SourceFile.EnumerateDominantPsiFiles();
        }
    }
}
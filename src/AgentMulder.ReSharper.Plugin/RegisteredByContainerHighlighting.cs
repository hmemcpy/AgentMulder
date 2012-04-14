using JetBrains.ReSharper.Daemon;

namespace AgentMulder.ReSharper.Plugin
{
    [StaticSeverityHighlighting(Severity.INFO, "GutterMarks", OverlapResolve = OverlapResolveKind.NONE, AttributeId = "Container Registration", ShowToolTipInStatusBar = false)]
    public sealed class RegisteredByContainerHighlighting : IHighlighting
    {
        private readonly ContainerInfo containerInfo;

        public RegisteredByContainerHighlighting(ContainerInfo containerInfo)
        {
            this.containerInfo = containerInfo;
        }

        public bool IsValid()
        {
            return true;
        }

        public string ToolTip
        {
            get { return string.Format("Registered by {0} in file: '{1}'", containerInfo.ContainerName, containerInfo.LocationName); }
        }

        public string ErrorStripeToolTip
        {
            get { return "Blah"; }
        }

        public int NavigationOffsetPatch
        {
            get { return 0; }
        }
    }
}
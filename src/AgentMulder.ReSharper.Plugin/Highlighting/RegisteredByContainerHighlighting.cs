using AgentMulder.ReSharper.Plugin.Components;
using JetBrains.Application;
using JetBrains.DocumentModel;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Feature.Services.Navigation.NavigationExtensions;
using JetBrains.ReSharper.Resources.Shell;
using JetBrains.ReSharper.Psi.Tree;


namespace AgentMulder.ReSharper.Plugin.Highlighting
{
    [StaticSeverityHighlighting(Severity.INFO, "GutterMarks", OverlapResolve = OverlapResolveKind.NONE, AttributeId = "Container Registration", ShowToolTipInStatusBar = false)]
    public class RegisteredByContainerHighlighting : IClickableGutterHighlighting
    {
        private readonly RegistrationInfo registrationInfo;
        private readonly string containerName;

        public RegisteredByContainerHighlighting(RegistrationInfo registrationInfo)
        {
            this.registrationInfo = registrationInfo;

            containerName = string.IsNullOrWhiteSpace(registrationInfo.ContainerDisplayName)
                                ? "a DI Container"
                                : registrationInfo.ContainerDisplayName;
        }

        public bool IsValid()
        {
            return registrationInfo.Registration.RegistrationElement.IsValid();
        }

        public DocumentRange CalculateRange()
        {
            return registrationInfo.Registration.RegistrationElement.GetDocumentRange();
        }

        public string ToolTip
        {
            get { return string.Format("Registered by {0} (click to navigate)", containerName); }
        }

        public string ErrorStripeToolTip
        {
            get { return "Blah"; }
        }

        public int NavigationOffsetPatch
        {
            get { return 0; }
        }

        public void OnClick()
        {
            using (ReadLockCookie.Create())
            {
                NavigateTo(registrationInfo.Registration.RegistrationElement);
            }
        }

        private void NavigateTo(ITreeNode registrationElement)
        {
            registrationElement.NavigateToTreeNode(true);
        }
    }
}
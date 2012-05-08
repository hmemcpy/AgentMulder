using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.Application;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Feature.Services.Navigation;

namespace AgentMulder.ReSharper.Plugin.Highlighting
{
    [StaticSeverityHighlighting(Severity.INFO, "GutterMarks", OverlapResolve = OverlapResolveKind.NONE, AttributeId = "Container Registration", ShowToolTipInStatusBar = false)]
    public sealed class RegisteredByContainerHighlighting : IClickableGutterHighlighting
    {
        private readonly IComponentRegistration registration;

        public RegisteredByContainerHighlighting(IComponentRegistration registration)
        {
            this.registration = registration;
        }

        public bool IsValid()
        {
            return registration.RegistrationElement.IsValid();
        }

        public string ToolTip
        {
            // todo add container name
            get { return "Registered by a DI Container (click to navigate)"; }
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
            ISolution currentSolution = Shell.Instance.GetComponent<ISolutionManager>().CurrentSolution;
            if (currentSolution == null)
            {
                return;
            }

            NavigationManager.Navigate(registration.RegistrationElement.GetNavigationRange(), currentSolution, true);
        }
    }
}
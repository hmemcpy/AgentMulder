using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Feature.Services.Navigation;

namespace AgentMulder.ReSharper.Plugin.Highlighting
{
    [StaticSeverityHighlighting(Severity.INFO, "GutterMarks", OverlapResolve = OverlapResolveKind.NONE, AttributeId = "Container Registration", ShowToolTipInStatusBar = false)]
    public sealed class RegisteredByContainerHighlighting : IClickableGutterHighlighting
    {
        private readonly ISolution solution;
        private readonly IComponentRegistration registration;

        public RegisteredByContainerHighlighting(ISolution solution, IComponentRegistration registration)
        {
            this.solution = solution;
            this.registration = registration;
        }

        public bool IsValid()
        {
            return registration.DocumentRange.IsValid();
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
            NavigationManager.Navigate(registration.DocumentRange, solution, true);
        }
    }
}
using JetBrains.ReSharper.Feature.Services.Navigation.Navigation.NavigationExtensions;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.ReSharper.Plugin.Highlighting
{
    public partial class RegisteredByContainerHighlighting
    {
        private void NavigateTo(ITreeNode registrationElement)
        {
            registrationElement.NavigateToTreeNode(true);
        }
    }
}
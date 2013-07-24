using JetBrains.ReSharper.Feature.Services.Navigation;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.ReSharper.Plugin.Highlighting
{
    public partial class RegisteredByContainerHighlighting
    {
        private void NavigateTo(ITreeNode registrationElement)
        {
            NavigationManager.Navigate(registrationElement, true);
        }
    }
}
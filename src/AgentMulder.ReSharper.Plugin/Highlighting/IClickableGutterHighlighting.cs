using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Feature.Services.Daemon;

namespace AgentMulder.ReSharper.Plugin.Highlighting
{
    public interface IClickableGutterHighlighting : IHighlighting
    {
        void OnClick();
    }
}
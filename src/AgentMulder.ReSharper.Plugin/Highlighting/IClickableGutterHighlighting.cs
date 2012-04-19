using JetBrains.ReSharper.Daemon;

namespace AgentMulder.ReSharper.Plugin.Highlighting
{
    public interface IClickableGutterHighlighting : IHighlighting
    {
        void OnClick();
    }
}
using JetBrains.ReSharper.Daemon;
#if SDK90
using JetBrains.ReSharper.Feature.Services.Daemon;
#endif

namespace AgentMulder.ReSharper.Plugin.Highlighting
{
    public interface IClickableGutterHighlighting : IHighlighting
    {
        void OnClick();
    }
}
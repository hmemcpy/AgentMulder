using JetBrains.ProjectModel;
using JetBrains.TextControl.Markup;

namespace AgentMulder.ReSharper.Plugin.Highlighting
{
    public partial class ContainerGutterMark : IconGutterMark
    {
        public override void OnClick(IHighlighter highlighter)
        {
            ISolution currentSolution = GetCurrentSolution();
            if (currentSolution == null)
            {
                return;
            }

            var clickable = JetBrains.ReSharper.Daemon.Daemon.GetInstance(currentSolution).GetHighlighting(highlighter) as IClickableGutterHighlighting;
            if (clickable != null)
            {
                clickable.OnClick();
            }
        }

        public override bool IsClickable
        {
            get { return true; }
        }
    }
}
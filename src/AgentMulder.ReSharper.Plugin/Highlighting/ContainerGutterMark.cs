using AgentMulder.ReSharper.Plugin.Highlighting;
using JetBrains.Application;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Intentions.Asp.Generate;
using JetBrains.TextControl.Markup;
using JetBrains.UI;

[assembly: RegisterHighlighter("Container Registration", "{B57372C1-16C3-4CB5-8B68-A0FBEFB487AD}", EffectType = EffectType.GUTTER_MARK, GutterMarkType = typeof(ContainerGutterMark), Layer = 2001)]

namespace AgentMulder.ReSharper.Plugin.Highlighting
{
    public class ContainerGutterMark : IconGutterMark
    {
        public ContainerGutterMark()
            : base(ImageLoader.GetImage("masterpage", typeof(AspGenerateContentItemProvider).Assembly))
        {
        }

        public override void OnClick(IHighlighter highlighter)
        {
            ISolution currentSolution = Shell.Instance.GetComponent<ISolutionManager>().CurrentSolution;
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
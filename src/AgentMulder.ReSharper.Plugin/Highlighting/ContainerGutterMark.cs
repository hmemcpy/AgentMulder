using System.Reflection;
using AgentMulder.ReSharper.Plugin.Highlighting;
using JetBrains.Application;
using JetBrains.ProjectModel;
using JetBrains.ProjectModel.Impl;
using JetBrains.TextControl.Markup;
using JetBrains.UI;

#if SDK70
using JetBrains.ReSharper.Features.Altering.Resources;
using JetBrains.UI.Icons;
#endif

[assembly: RegisterHighlighter("Container Registration", "{B57372C1-16C3-4CB5-8B68-A0FBEFB487AD}", EffectType = EffectType.GUTTER_MARK, GutterMarkType = typeof(ContainerGutterMark), Layer = 2001)]

namespace AgentMulder.ReSharper.Plugin.Highlighting
{
    public class ContainerGutterMark : IconGutterMark
    {
        
#if SDK70
        // todo move this resorce to this assembly
        public ContainerGutterMark(IThemedIconManager iconManager)
            : base(AlteringFeatuThemedIcons.GeneratedMembers.Id, iconManager)
#else
        public ContainerGutterMark()
            : base(ImageLoader.GetImage("Hat", Assembly.GetExecutingAssembly()))
#endif
        {
        }

        public override void OnClick(IHighlighter highlighter)
        {
#if SDK70
            ISolution currentSolution = Shell.Instance.GetComponent<SolutionsManager>().Solution;
#else
            ISolution currentSolution = Shell.Instance.GetComponent<ISolutionManager>().CurrentSolution;
#endif
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
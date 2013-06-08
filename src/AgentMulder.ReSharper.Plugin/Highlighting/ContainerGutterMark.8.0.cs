using AgentMulder.ReSharper.Plugin.Highlighting;
using JetBrains.Application;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Features.Altering.Resources;
using JetBrains.TextControl.DocumentMarkup;
using JetBrains.UI.Icons;

[assembly: RegisterHighlighter("Container Registration", EffectType = EffectType.GUTTER_MARK, GutterMarkType = typeof(ContainerGutterMark), Layer = 2001)]

namespace AgentMulder.ReSharper.Plugin.Highlighting
{
    public partial class ContainerGutterMark
    {
        public ContainerGutterMark(IThemedIconManager iconManager)
            : base(AlteringFeatuThemedIcons.GeneratedMembers.Id, iconManager)
        {
        }

        private ISolution GetCurrentSolution()
        {
            return Shell.Instance.GetComponent<SolutionsManager>().Solution;
        }
    }
}
using System;
using System.Collections.Generic;
using AgentMulder.ReSharper.Plugin.Highlighting;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Feature.Services.Resources;
using JetBrains.ReSharper.Resources.Shell;
using JetBrains.TextControl.DocumentMarkup;
using JetBrains.UI.BulbMenu;
using JetBrains.UI.Icons;

[assembly: RegisterHighlighter("Container Registration", EffectType = EffectType.GUTTER_MARK, GutterMarkType = typeof(ContainerGutterMark), Layer = 2001)]

namespace AgentMulder.ReSharper.Plugin.Highlighting
{
    public class ContainerGutterMark : IconGutterMark
    {
        public ContainerGutterMark(IThemedIconManager iconManager)
            : base(AlteringFeatuThemedIcons.GeneratedMembers.Id, iconManager)
        {
        }

        private ISolution GetCurrentSolution()
        {
            return Shell.Instance.GetComponent<SolutionsManager>().Solution;
        }

        public override IEnumerable<BulbMenuItem> GetBulbMenuItems(IHighlighter highlighter)
        {
            yield return new BulbMenuItem(new ExecutableItem(() =>
            {
                ISolution solution = GetCurrentSolution();
                if (solution == null)
                {
                    return;
                }

                var clickable = solution.GetComponent<IDaemon>().GetHighlighting(highlighter) as IClickableGutterHighlighting;
                if (clickable == null)
                {
                    return;
                }

                clickable.OnClick();

            }), highlighter.ToolTip, IconId, BulbMenuAnchorPositions.PermanentBackgroundItems, true);
        }
    }
}
using System.Reflection;
using AgentMulder.ReSharper.Plugin.Highlighting;
using JetBrains.Application;
using JetBrains.ProjectModel;
using JetBrains.TextControl.Markup;
using JetBrains.UI;

[assembly: RegisterHighlighter("Container Registration", "{B57372C1-16C3-4CB5-8B68-A0FBEFB487AD}", EffectType = EffectType.GUTTER_MARK, GutterMarkType = typeof(ContainerGutterMark), Layer = 2001)]

namespace AgentMulder.ReSharper.Plugin.Highlighting
{
    public partial class ContainerGutterMark
    {
        public ContainerGutterMark()
            : base(ImageLoader.GetImage("Hat", Assembly.GetExecutingAssembly()))
        {
        }

        private ISolution GetCurrentSolution()
        {
            return Shell.Instance.GetComponent<ISolutionManager>().CurrentSolution;
        }
    }
}
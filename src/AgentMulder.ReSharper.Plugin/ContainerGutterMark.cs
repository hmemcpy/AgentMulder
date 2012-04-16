using AgentMulder.ReSharper.Plugin;
using JetBrains.ReSharper.Intentions.Asp.Generate;
using JetBrains.TextControl.Markup;
using JetBrains.UI;

[assembly: RegisterHighlighter("Container Registration", "{B57372C1-16C3-4CB5-8B68-A0FBEFB487AD}", EffectType = EffectType.GUTTER_MARK, GutterMarkType = typeof(ContainerGutterMark), Layer = 2001)]

namespace AgentMulder.ReSharper.Plugin
{
    public class ContainerGutterMark : IconGutterMark
    {
        public ContainerGutterMark()
            : base(ImageLoader.GetImage("masterpage", typeof(AspGenerateContentItemProvider).Assembly))
        {
        }

        public override void OnClick(IHighlighter highlighter)
        {
            
        }

        public override bool IsClickable
        {
            get { return true; }
        }
    }
}
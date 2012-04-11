using System;
using JetBrains.Application.Progress;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.Bulbs;
using JetBrains.ReSharper.Feature.Services.CSharp.Bulbs;
using JetBrains.ReSharper.Feature.Services.LinqTools;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.TextControl;
using JetBrains.Util;

namespace AgentMulder.ReSharper.Plugin
{
    /// <summary>
    /// This is an example context action. The test project demonstrates tests for
    /// availability and execution of this action.
    /// </summary>
    [ContextAction(Name = "ReverseString", Description = "Reverses a string", Group = "C#")]
    public class ReverseStringAction : BulbItemImpl, IContextAction
    {
        private readonly ICSharpContextActionDataProvider provider;
        private ILiteralExpression stringLiteral;

        public ReverseStringAction(ICSharpContextActionDataProvider provider)
        {
            this.provider = provider;
        }

        public bool IsAvailable(IUserDataHolder cache)
        {
            var literal = this.provider.GetSelectedElement<ILiteralExpression>(true, true);
            if (literal != null && literal.IsConstantValue() && literal.ConstantValue.IsString())
            {
                var s = literal.ConstantValue.Value as string;
                if (!string.IsNullOrEmpty(s))
                {
                    this.stringLiteral = literal;
                    return true;
                }
            }
            return false;
        }

        protected override Action<ITextControl> ExecutePsiTransaction(ISolution solution, IProgressIndicator progress)
        {
            CSharpElementFactory factory = CSharpElementFactory.GetInstance(this.provider.PsiModule);

            var stringValue = this.stringLiteral.ConstantValue.Value as string;
            if (stringValue == null)
            {
                return null;
            }

            var chars = stringValue.ToCharArray();
            Array.Reverse(chars);
            ICSharpExpression newExpr = factory.CreateExpressionAsIs("\"" + new string(chars) + "\"");
            this.stringLiteral.ReplaceBy(newExpr);
            return null;
        }

        public new IBulbItem[] Items
        {
            get
            {
                return new IBulbItem[] { this };
            }
        }

        public override string Text
        {
            get { return "Reverse string"; }
        }
    }
}
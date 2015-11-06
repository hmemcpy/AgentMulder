using System.Collections.Generic;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Modules;
using JetBrains.ReSharper.Psi.Resolve.Managed;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.Autofac.Patterns.Helpers
{
    public class ReturnTypeCollector : TreeNodeVisitor, IRecursiveElementProcessor
    {
        private readonly List<IExpressionType> expressionTypes = new List<IExpressionType>();
        private readonly IResolveContext resolveContext;

        public IEnumerable<IExpressionType> CollectedTypes
        {
            get { return expressionTypes; }
        }

        public bool ProcessingIsFinished
        {
            get { return false; }
        }

        public ReturnTypeCollector(IResolveContext resolveContext)
        {
            this.resolveContext = resolveContext;
        }

        public bool InteriorShouldBeProcessed(ITreeNode element)
        {
            return !(element is IReturnStatement);
        }

        public void ProcessBeforeInterior(ITreeNode element)
        {
            var csharpTreeNode = element as ICSharpTreeNode;
            if (csharpTreeNode != null)
                csharpTreeNode.Accept(this);
            else
                VisitNode(element);
        }

        public void ProcessAfterInterior(ITreeNode element)
        {
        }

        public override void VisitReturnStatement(IReturnStatement returnStatementParam)
        {
            ICSharpExpression csharpExpression = returnStatementParam.Value;
            expressionTypes.Add(csharpExpression != null
                                    ? csharpExpression.GetExpressionType(resolveContext)
                                    : GetVoidType(returnStatementParam.GetPsiModule()));

            base.VisitReturnStatement(returnStatementParam);
        }

        private IDeclaredType GetVoidType(IPsiModule module)
        {
            return module.GetPredefinedType().Void;
        }

    }
}
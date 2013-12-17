using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.ReSharper.Domain.Utils
{
    public static partial class ContainerExtensions
    {
        private static IDeclaredType CreateTypeByClrName(ITreeNode node, string containerClrTypeName)
        {
            return TypeFactory.CreateTypeByCLRName(containerClrTypeName, node.GetPsiModule(), node.GetResolveContext());
        }
    }
}
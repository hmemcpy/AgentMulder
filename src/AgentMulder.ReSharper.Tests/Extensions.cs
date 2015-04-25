using System;
using JetBrains.Annotations;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.ReSharper.Tests
{
    internal static class Extensions
    {
        public static void ProcessChildren<T>(this ITreeNode element, [InstantHandle] Action<T> handler) where T : class, ITreeNode
        {
            foreach (T treeNode in element.ThisAndDescendants<T>())
            {
                if (treeNode != null)
                    handler(treeNode);
            }
        }
    }
}
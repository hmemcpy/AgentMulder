using System.Collections.Generic;
using JetBrains.ReSharper.Psi;

namespace AgentMulder.Containers.CastleWindsor.Helpers
{
    public static class NamespaceExtractor
    {
        private static readonly List<INamespaceExtractor> extractors = new List<INamespaceExtractor>
        {
            new InNamespaceExtractor(),
            new InSameNamespaceAsGenericExtractor(),
            new InSameNamespaceAsNonGenericExtractor()
        };

        public static INamespace GetNamespace<T>(T element, out bool includeSubNamespaces)
        {
            foreach (var namespaceExtractor in extractors)
            {
                var result = namespaceExtractor.GetNamespace(element, out includeSubNamespaces);
                if (result != null)
                {
                    return result;
                }
            }

            includeSubNamespaces = false;
            return null;
        }
    }
}
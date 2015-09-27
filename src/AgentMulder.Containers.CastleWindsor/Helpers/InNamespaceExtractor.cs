using System;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using PsiExtensions = AgentMulder.ReSharper.Domain.Utils.PsiExtensions;

namespace AgentMulder.Containers.CastleWindsor.Helpers
{
    internal class InNamespaceExtractor : INamespaceExtractor
    {
        public INamespace GetNamespace<T>(T element, out bool includeSubnamespaces)
        {
            var arguments = element as ICSharpArgument[];
            if (arguments == null)
            {
                includeSubnamespaces = false;
                return null;
            }

            return ExtractNamespaceElement(arguments, out includeSubnamespaces);
        }

        private static INamespace ExtractNamespaceElement(ICSharpArgument[] arguments, out bool includeSubnamespaces)
        {
            INamespace namespaceElement = null;
            if (arguments.Length > 0)
            {
                namespaceElement = PsiExtensions.GetNamespaceDeclaration(arguments[0].Value);
            }

            includeSubnamespaces = false;
            if (arguments.Length == 2)
            {
                ICSharpArgument boolArgument = arguments[1];
                if (boolArgument.Value.ConstantValue != null &&
                    boolArgument.Value.ConstantValue.IsBoolean())
                {
                    includeSubnamespaces = Convert.ToBoolean(boolArgument.Value.ConstantValue.Value);
                }
            }

            return namespaceElement;
        }
    }
}
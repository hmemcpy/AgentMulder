using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.CSharp.Util;
using JetBrains.ReSharper.Psi.Resolve.ExtensionMethods;

namespace AgentMulder.ReSharper.Domain.Modules.Impl
{
    internal class GetExecutingAssemblyExtractor : IModuleExtractor
    {
        public IModule GetTargetModule<T>(T element)
        {
            var invocationExpression = element as IInvocationExpression;
            if (invocationExpression != null)
            {
                ExtensionInstance<IMethod> method = InvocationExpressionUtil.GetInvokedMethod(invocationExpression);
                if (method == null)
                {
                    return null;
                }

                // todo horrible horrible hack, fix this later
                if (method.Element.XMLDocId == "M:System.Reflection.Assembly.GetExecutingAssembly")
                {
                    return invocationExpression.GetPsiModule().ContainingProjectModule;
                }
            }
            return null;
        }
    }
}
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace AgentMulder.ReSharper.Domain.Modules
{
    internal class TypeofExtractor : IModuleExtractor
    {
        public IModule GetTargetModule(ICSharpExpression expression)
        {
            var referenceExpression = expression as IReferenceExpression;
            if (referenceExpression != null)
            {
                var typeofExpression = referenceExpression.QualifierExpression as ITypeofExpression;
                if (typeofExpression != null)
                {
                    return typeofExpression.ArgumentType.Module.ContainingProjectModule;
                }
            }

            return null;
        }
    }
}
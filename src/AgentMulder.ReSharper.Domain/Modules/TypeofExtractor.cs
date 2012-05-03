using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace AgentMulder.ReSharper.Domain.Modules
{
    internal class TypeOfExtractor : IModuleExtractor
    {
        public IModule GetTargetModule(ICSharpExpression expression)
        {
            var referenceExpression = expression as IReferenceExpression;
            if (referenceExpression != null)
            {
                var typeofExpression = referenceExpression.QualifierExpression as ITypeofExpression;
                if (typeofExpression != null)
                {
                    IDeclaredType declaredType = typeofExpression.ArgumentType.GetScalarType();
                    if (declaredType != null)
                    {
                        ITypeElement typeElement = declaredType.GetTypeElement();
                        if (typeElement != null)
                        {
                            return typeElement.Module.ContainingProjectModule;
                        }
                    }
                }
            }

            return null;
        }
    }
}


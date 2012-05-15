using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;

namespace AgentMulder.ReSharper.Domain.Modules.Impl
{
    internal class TypeElementExtractor : IModuleExtractor
    {
        public IModule GetTargetModule<T>(T element)
        {
            var type = element as IType;
            if (type == null)
            {
                return null;
            }

            IDeclaredType declaredType = type.GetScalarType();
            if (declaredType != null)
            {
                ITypeElement typeElement = declaredType.GetTypeElement();
                if (typeElement != null)
                {
                    return typeElement.Module.ContainingProjectModule;
                }
            }

            return null;
        }
    }
}
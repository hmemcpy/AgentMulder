using AgentMulder.ReSharper.Domain.Elements;
using AgentMulder.ReSharper.Domain.Elements.Modules;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Resolve;

namespace AgentMulder.Containers.Autofac.Patterns.Helpers
{
    public class AutofacModuleThisAssemblyExtractor : IModuleExtractor
    {
        private static readonly IClrTypeName autofacModuleName = new ClrTypeName("Autofac.Module");

        public IModule GetTargetModule<T>(T element)
        {
            var referenceExpression = element as IReferenceExpression;
            if (referenceExpression == null)
            {
                return null;
            }

            if (referenceExpression.Reference.GetName() == "ThisAssembly")
            {
                IResolveResult resolveResult = referenceExpression.Reference.Resolve().Result;
                IDeclaredElement declaredElement = resolveResult.DeclaredElement;
                if (declaredElement == null)
                {
                    return null;
                }

                var property = declaredElement as IProperty;
                if (property == null)
                {
                    return null;
                }

                ITypeElement propertyType = property.GetContainingType();
                if (propertyType == null)
                {
                    return null;
                }

                if (!propertyType.GetClrName().Equals(autofacModuleName))
                {
                    return null;
                }

                return referenceExpression.GetPsiModule().ContainingProjectModule;
            }

            return null;
        }
        
        IModule IElementExtractor<IModule>.ExtractElement<TElement>(TElement element)
        {
            return GetTargetModule(element);
        }
    }
}
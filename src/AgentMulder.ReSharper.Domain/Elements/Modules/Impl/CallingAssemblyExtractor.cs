#if SDK90
using JetBrains.Metadata.Reader.API;
using JetBrains.Metadata.Reader.Impl;
#endif
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Resolve;

namespace AgentMulder.ReSharper.Domain.Elements.Modules.Impl
{
    public class CallingAssemblyExtractor : IModuleExtractor
    {
        private readonly string methodName;
        private readonly IClrTypeName clrTypeName;

        public CallingAssemblyExtractor(string typeName, string methodName)
        {
            this.methodName = methodName;
            clrTypeName = new ClrTypeName(typeName);
        }

        public IModule GetTargetModule<T>(T element)
        {
            var referenceExpression = element as IReferenceExpression;
            if (referenceExpression == null)
            {
                return null;
            }

            if (referenceExpression.Reference.GetName() == methodName)
            {
                IResolveResult resolveResult = referenceExpression.Reference.Resolve().Result;
                IDeclaredElement declaredElement = resolveResult.DeclaredElement;
                if (declaredElement == null)
                {
                    return null;
                }

                var clrDeclaredElement = declaredElement as IClrDeclaredElement;
                if (clrDeclaredElement == null)
                {
                    return null;
                }

                ITypeElement clrType = clrDeclaredElement.GetContainingType();
                if (clrType == null)
                {
                    return null;
                }

                if (!clrType.GetClrName().Equals(clrTypeName))
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
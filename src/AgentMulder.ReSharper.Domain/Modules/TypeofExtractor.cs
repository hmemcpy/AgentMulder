using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace AgentMulder.ReSharper.Domain.Modules
{
    internal class TypeOfExtractor : IModuleExtractor
    {
        public IModule GetTargetModule(ICSharpExpression expression)
        {
            return this.With(x => expression as IReferenceExpression)
                .With(x => x.QualifierExpression as ITypeofExpression)
                .With(x => x.ArgumentType.GetScalarType())
                .With(x => x.GetTypeElement())
                .Return(x => x.Module.ContainingProjectModule, null);
        }
    }
}


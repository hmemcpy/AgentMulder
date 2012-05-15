using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace AgentMulder.ReSharper.Domain.Modules.Impl
{
    internal class TypeAssemblyPropertyExtractor : ModuleExtractorDecorator
    {
        public TypeAssemblyPropertyExtractor(TypeOfExtractor typeOfExtractor)
            : base(typeOfExtractor)
        {
        }

        public override IModule GetTargetModule<T>(T element)
        {
            var referenceExpression = element as IReferenceExpression;
            if (referenceExpression != null && referenceExpression.Reference.GetName() == "Assembly")
            {
                return base.GetTargetModule(referenceExpression.QualifierExpression);
            }

            return null;
        }
    }
}


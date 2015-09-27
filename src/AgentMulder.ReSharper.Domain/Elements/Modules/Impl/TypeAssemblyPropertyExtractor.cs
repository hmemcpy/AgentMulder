using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace AgentMulder.ReSharper.Domain.Elements.Modules.Impl
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
            if (referenceExpression == null)
            {
                return null;
            }

            switch (referenceExpression.Reference.GetName())
            {
                case "Assembly":
                    return base.GetTargetModule(referenceExpression.QualifierExpression);
                default:
                    return null;
            }
        }
    }
}


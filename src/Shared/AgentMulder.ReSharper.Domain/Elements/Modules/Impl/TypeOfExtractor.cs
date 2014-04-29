using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace AgentMulder.ReSharper.Domain.Elements.Modules.Impl
{
    internal class TypeOfExtractor : ModuleExtractorDecorator
    {
        public TypeOfExtractor(TypeElementExtractor typeElementExtractor)
            : base(typeElementExtractor)
        {
        }

        public override IModule GetTargetModule<T>(T element)
        {
            var typeofExpression = element as ITypeofExpression;
            if (typeofExpression != null)
            {
                return base.GetTargetModule(typeofExpression.ArgumentType);
            }

            return null;
        }
    }
}
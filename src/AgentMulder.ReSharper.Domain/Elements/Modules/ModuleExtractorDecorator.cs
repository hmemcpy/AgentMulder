using JetBrains.ProjectModel;

namespace AgentMulder.ReSharper.Domain.Elements.Modules
{
    internal abstract class ModuleExtractorDecorator : ElementExtractorDecorator<IModule>, IModuleExtractor
    {
        protected ModuleExtractorDecorator(IElementExtractor<IModule> baseExtractor)
            : base(baseExtractor)
        {
        }

        public virtual IModule GetTargetModule<T>(T element)
        {
            return base.ExtractElement(element);
        }

        public override IModule ExtractElement<T>(T element)
        {
            return GetTargetModule(element);
        }
    }
}
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.ReSharper.Domain.Modules
{
    public abstract class ModuleExtractorDecorator : IModuleExtractor
    {
        private readonly IModuleExtractor baseExtractor;

        protected ModuleExtractorDecorator(IModuleExtractor baseExtractor)
        {
            this.baseExtractor = baseExtractor;
        }

        public virtual IModule GetTargetModule<T>(T element)
        {
            return baseExtractor.GetTargetModule(element);
        }
    }
}
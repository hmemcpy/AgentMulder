using JetBrains.ProjectModel;

namespace AgentMulder.ReSharper.Domain.Elements.Modules
{
    public interface IModuleExtractor : IElementExtractor<IModule>
    {
        IModule GetTargetModule<T>(T element);
    }
}
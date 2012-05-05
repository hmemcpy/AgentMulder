using JetBrains.ProjectModel;

namespace AgentMulder.ReSharper.Domain.Modules
{
    public interface IModuleExtractor
    {
        IModule GetTargetModule<T>(T element);
    }
}
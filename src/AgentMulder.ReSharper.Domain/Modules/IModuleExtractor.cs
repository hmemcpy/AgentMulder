using JetBrains.ReSharper.Psi;

namespace AgentMulder.ReSharper.Domain.Modules
{
    public interface IModuleExtractor
    {
        IModule GetTargetModule<T>(T element);
    }
}
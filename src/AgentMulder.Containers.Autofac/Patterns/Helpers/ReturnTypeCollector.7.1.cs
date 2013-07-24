using JetBrains.ReSharper.Psi;

namespace AgentMulder.Containers.Autofac.Patterns.Helpers
{
    public partial class ReturnTypeCollector
    {
        private IDeclaredType GetVoidType(IPsiModule module)
        {
            return module.GetPredefinedType().Void;
        }
    }
}
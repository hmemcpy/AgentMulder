using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Modules;

namespace AgentMulder.Containers.Autofac.Patterns.Helpers
{
    public partial class ReturnTypeCollector
    {
        private IDeclaredType GetVoidType(IPsiModule module)
        {
            return module.GetPredefinedType(module.GetContextFromModule()).Void;
        }
    }
}
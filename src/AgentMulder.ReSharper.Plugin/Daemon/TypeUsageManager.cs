using JetBrains.ReSharper.Daemon.UsageChecking;
using JetBrains.ReSharper.Psi;

namespace AgentMulder.ReSharper.Plugin.Daemon
{
    internal class TypeUsageManager : ITypeUsageManager
    {
        private readonly CollectUsagesStageProcess usagesStageProcess;

        public TypeUsageManager(CollectUsagesStageProcess usagesStageProcess)
        {
            this.usagesStageProcess = usagesStageProcess;
        }

        public void MarkTypeUsed(ITypeElement typeElement)
        {
            MarkConstructorsUsed(typeElement);

            usagesStageProcess.SetElementState(typeElement, UsageState.ACCESSED);
        }

        private void MarkConstructorsUsed(ITypeElement typeElement)
        {
            foreach (IConstructor constructor in typeElement.Constructors)
            {
                usagesStageProcess.SetElementState(constructor,
                                                   UsageState.CANNOT_BE_PROTECTED | UsageState.CANNOT_BE_INTERNAL |
                                                   UsageState.CANNOT_BE_PRIVATE | UsageState.USED_MASK);
            }
        }
    }
}
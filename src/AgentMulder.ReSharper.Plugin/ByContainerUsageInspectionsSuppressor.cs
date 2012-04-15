using JetBrains.Annotations;
using JetBrains.Application;
using JetBrains.ReSharper.Daemon.UsageChecking;
using JetBrains.ReSharper.Psi;

namespace AgentMulder.ReSharper.Plugin
{
    [ShellComponent]
    public class ByContainerUsageInspectionsSuppressor : IUsageInspectionsSupressor
    {
        public bool SupressUsageInspectionsOnElement(IDeclaredElement element, out ImplicitUseKindFlags flags)
        {
            if (element.ShortName.Contains("Writer"))
            {
                flags = ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature;
                return true;
            }

            flags = ImplicitUseKindFlags.Default;
            return false;
        }
    }
}
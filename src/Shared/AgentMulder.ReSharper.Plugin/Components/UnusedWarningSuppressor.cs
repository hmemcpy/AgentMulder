using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using JetBrains.Application;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Daemon.UsageChecking;
using JetBrains.ReSharper.Psi;

namespace AgentMulder.ReSharper.Plugin.Components
{
    [ShellComponent]
    public class UnusedWarningSuppressor : IUsageInspectionsSuppressor
    {
        public bool SuppressUsageInspectionsOnElement(IDeclaredElement element, out ImplicitUseKindFlags flags)
        {
            flags = ImplicitUseKindFlags.Default;
            ITypeElement typeElement = element as ITypeElement;
            if (typeElement == null)
                return false;

            var patternManager = typeElement.GetSolution().GetComponent<IPatternManager>();
            IPsiSourceFile file = typeElement.GetSingleOrDefaultSourceFile();
            IEnumerable<RegistrationInfo> registrations = patternManager.GetRegistrationsForFile(file);

            if (registrations.Any(info => info.Registration.IsSatisfiedBy(typeElement)))
            {
                flags = ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature;
                return true;
            }

            return false;
        }
    }
}
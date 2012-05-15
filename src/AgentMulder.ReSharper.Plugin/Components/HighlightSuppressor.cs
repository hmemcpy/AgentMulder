using System.Linq;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.Annotations;
using JetBrains.Application;
using JetBrains.ReSharper.Daemon.UsageChecking;
using JetBrains.ReSharper.Psi;

namespace AgentMulder.ReSharper.Plugin.Components
{
    [ShellComponent]
    public class HighlightSuppressor : IUsageInspectionsSupressor
    {
        private readonly SolutionAnalyzer solutionAnalyzer;

        public HighlightSuppressor(SolutionAnalyzer solutionAnalyzer)
        {
            this.solutionAnalyzer = solutionAnalyzer;
        }

        public bool SupressUsageInspectionsOnElement(IDeclaredElement element, out ImplicitUseKindFlags flags)
        {
            var typeElement = element as ITypeElement;
            if (typeElement != null && typeElement.IsClassLike())
            {
                var componentRegistrations = solutionAnalyzer.Analyze();
                IComponentRegistration registration = componentRegistrations.FirstOrDefault(r => r.IsSatisfiedBy(typeElement));
                if (registration != null)
                {
                    flags = ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature;
                    return true;
                }
            }

            flags = ImplicitUseKindFlags.Default;
            return false;
        }
    }
}
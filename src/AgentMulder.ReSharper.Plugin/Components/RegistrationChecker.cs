using System.Linq;
using AgentMulder.ReSharper.Domain.Registrations;
using AgentMulder.ReSharper.Plugin.Highlighting;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Daemon.Stages;
using JetBrains.ReSharper.Daemon.Stages.Dispatcher;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.ReSharper.Plugin.Components
{
    //[ElementProblemAnalyzer(new[] { typeof(ITypeDeclaration) }, HighlightingTypes = new[] { typeof(RegisteredByContainerHighlighting) })]
    //public class RegistrationChecker : IElementProblemAnalyzer
    //{
    //    private readonly SolutionAnalyzer solutionAnalyzer;

    //    public RegistrationChecker(SolutionAnalyzer solutionAnalyzer)
    //    {
    //        this.solutionAnalyzer = solutionAnalyzer;
    //    }

    //    public void Run(ITreeNode element, ElementProblemAnalyzerData data, IHighlightingConsumer consumer)
    //    {
    //        if (data.ProcessKind == DaemonProcessKind.VISIBLE_DOCUMENT)
    //        {
    //            var typeDeclaration = (ITypeDeclaration)element;

    //            var componentRegistrations = solutionAnalyzer.Analyze();
    //            IComponentRegistration registration = componentRegistrations.FirstOrDefault(r => r.IsSatisfiedBy(typeDeclaration.DeclaredElement));
    //            if (registration == null)
    //            {
    //                return;
    //            }

    //            consumer.AddHighlighting(new RegisteredByContainerHighlighting(registration),
    //                                     typeDeclaration.GetNameDocumentRange(),
    //                                     registration.RegistrationElement.GetContainingFile());
    //        }
    //    }
    //}
}
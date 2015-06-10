using System;
using System.Linq;
using AgentMulder.ReSharper.Plugin.Components;
using AgentMulder.ReSharper.Plugin.Highlighting;
using JetBrains.ReSharper.Daemon.Stages;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.ReSharper.Plugin.Daemon
{
    public partial class ContainerRegistrationAnalysisStageProcess
    {
        private void ProcessFile(IFile psiFile, DefaultHighlightingConsumer consumer)
        {
            psiFile.ProcessChildren<ITypeDeclaration>(declaration =>
            {
                if (declaration.DeclaredElement == null) // type is not (yet) declared
                {
                    return;
                }

                RegistrationInfo registrationInfo = patternManager.GetRegistrationsForFile(psiFile.GetSourceFile()).
                                                                   FirstOrDefault(c => c.Registration.IsSatisfiedBy(declaration.DeclaredElement));
                if (registrationInfo != null)
                {
                    IPsiSourceFile psiSourceFile = registrationInfo.GetSourceFile();
                    consumer.AddHighlighting(new RegisteredByContainerHighlighting(registrationInfo),
                        declaration.GetNameDocumentRange(),
                        psiSourceFile.GetTheOnlyPsiFile(psiSourceFile.PrimaryPsiLanguage));
                }
            });
        }

    }
}

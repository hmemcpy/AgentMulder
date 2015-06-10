using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AgentMulder.ReSharper.Plugin.Components;
using AgentMulder.ReSharper.Plugin.Highlighting;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.ReSharper.Plugin.Daemon
{
    public partial class ContainerRegistrationAnalysisStageProcess
    {
        private void ProcessFile(IFile psiFile, DefaultHighlightingConsumer consumer)
        {
            foreach (var declaration in psiFile.ThisAndDescendants<ITypeDeclaration>())
            {
                if (declaration.DeclaredElement == null) // type is not (yet) declared
                {
                    return;
                }

                RegistrationInfo registrationInfo = patternManager.GetRegistrationsForFile(psiFile.GetSourceFile()).
                                                                   FirstOrDefault(c => c.Registration.IsSatisfiedBy(declaration.DeclaredElement));
                if (registrationInfo != null)
                {
                    consumer.AddHighlighting(new RegisteredByContainerHighlighting(registrationInfo), declaration.GetNameDocumentRange(), psiFile);
                }
            }
        }

    }
}

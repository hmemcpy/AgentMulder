using System.Linq;
using AgentMulder.ReSharper.Plugin.Components;
using JetBrains.Application.DataContext;
using JetBrains.DocumentManagers;
using JetBrains.DocumentModel;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.ContextNavigation.Util;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Parsing;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.ReSharper.Plugin.Navigation
{
    public partial class RegisteredComponentsContextSearch
    {
        public bool IsContextApplicable(IDataContext dataContext)
        {
            return ContextNavigationUtil.CheckDefaultApplicability<CSharpLanguage>(dataContext);
        }

        private static bool IsSelectedElementAssociatedWithRegistration(IDataContext dataContext)
        {
            ISolution solution = dataContext.GetData(JetBrains.ProjectModel.DataContext.DataConstants.SOLUTION);
            if (solution == null)
                return false;
            IDocument document = dataContext.GetData(JetBrains.DocumentModel.DataConstants.DOCUMENT);
            if (document == null)
                return false;

            DocumentOffset documentOffset = dataContext.GetData(JetBrains.DocumentModel.DataConstants.DOCUMENT_OFFSET);
            if (documentOffset == null)
                return false;

            IPsiSourceFile psiSourceFile = document.GetPsiSourceFile(solution);
            if (psiSourceFile != null)
            {
                // todo make this resolvable also via the AllTypes... line
                var invokedNode = dataContext.GetSelectedTreeNode<IExpression>();

                return solution.GetComponent<IPatternManager>()
                               .GetRegistrationsForFile(psiSourceFile)
                               .Any(r => r.Registration.RegistrationElement.Children().Contains(invokedNode));
            }

            return false;
        }

    }
}
using System;
using System.Linq;
using AgentMulder.ReSharper.Plugin.Components;
using JetBrains.Application;
using JetBrains.Application.DataContext;
using JetBrains.DocumentManagers;
using JetBrains.DocumentModel;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.ContextNavigation.Util;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.ReSharper.Plugin.Navigation
{
#if SDK80
    [ShellFeaturePart]
#else
    [FeaturePart]
#endif
    public partial class RegisteredComponentsContextSearch : IRegisteredComponentsContextSearch
    {
        private readonly IShellLocks locks;

        public RegisteredComponentsContextSearch(/*SolutionAnalyzer solutionAnalyzer, */IShellLocks locks)
        {
            this.locks = locks;
        }

        public bool IsApplicable(IDataContext dataContext)
        {
            return ContextNavigationUtil.CheckDefaultApplicability<CSharpLanguage>(dataContext);
        }

        public bool IsAvailable(IDataContext dataContext)
        {
            return IsSelectedElementAssociatedWithRegistration(dataContext);
        }

        public RegisteredComponentsSearchRequest GetRegisteredComponentsRequest(IDataContext dataContext)
        {
            ISolution solution = dataContext.GetData(JetBrains.ProjectModel.DataContext.DataConstants.SOLUTION);
            if (solution == null)
            {
                throw new InvalidOperationException("Unable to get the solution");
            }

            var invokedNode = dataContext.GetSelectedTreeNode<IExpression>();


#if SDK70 || SDK80
            IDocument document = dataContext.GetData(JetBrains.DocumentModel.DataConstants.DOCUMENT);
#else
            IDocument document = dataContext.GetData(JetBrains.IDE.DataConstants.DOCUMENT);
#endif

            if (document == null)
                return null;

            IPsiSourceFile psiSourceFile = document.GetPsiSourceFile(solution);
            if (psiSourceFile == null)
            {
                return null;
            }

            var registration = solution.GetComponent<IPatternManager>().GetRegistrationsForFile(psiSourceFile)
                .FirstOrDefault(r => r.Registration.RegistrationElement.Children().Contains(invokedNode));
            if (registration == null)
            {
                return null;
            }

            return new RegisteredComponentsSearchRequest(solution, locks, registration.Registration);
        }
    }
}
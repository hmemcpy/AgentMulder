using System;
using System.Collections.Generic;
using System.Linq;
using AgentMulder.ReSharper.Domain.Registrations;
using AgentMulder.ReSharper.Plugin.Components;
using JetBrains.Application.DataContext;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.ContextNavigation.Util;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp;

namespace AgentMulder.ReSharper.Plugin.Navigation
{
    [FeaturePart]
    public class RegisteredComponentsContextSearch : IRegisteredComponentsContextSearch
    {
        private readonly SolutionAnalyzer solutionAnalyzer;

        public RegisteredComponentsContextSearch(SolutionAnalyzer solutionAnalyzer)
        {
            this.solutionAnalyzer = solutionAnalyzer;
        }

        public bool IsAvailable(IDataContext dataContext)
        {
            IEnumerable<IComponentRegistration> componentRegistrations = solutionAnalyzer.Analyze();
            
            return componentRegistrations.Any();
        }

        public bool IsApplicable(IDataContext dataContext)
        {
            return ContextNavigationUtil.CheckDefaultApplicability<CSharpLanguage>(dataContext);
        }

        public RegisteredComponentsSearchRequest GetRegisteredComponentsRequest(IDataContext dataContext)
        {
            ISolution solution = dataContext.GetData(JetBrains.ProjectModel.DataContext.DataConstants.SOLUTION);
            if (solution == null)
            {
                throw new InvalidOperationException("Unable to get the solution");
            }

            return new RegisteredComponentsSearchRequest(solution, solutionAnalyzer);
        }
    }
}
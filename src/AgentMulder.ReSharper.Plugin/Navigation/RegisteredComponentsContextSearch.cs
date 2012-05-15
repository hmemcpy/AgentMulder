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
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.ReSharper.Plugin.Navigation
{
    [FeaturePart]
    public class RegisteredComponentsContextSearch : IRegisteredComponentsContextSearch
    {
        // todo fixme needs a real cache
        private readonly IEnumerable<IComponentRegistration> cachedRegistrations;
        
        public RegisteredComponentsContextSearch(SolutionAnalyzer solutionAnalyzer)
        {
            cachedRegistrations = solutionAnalyzer.Analyze();
        }

        public bool IsAvailable(IDataContext dataContext)
        {
            // todo make this resolvable also via the AllTypes... line
            var invokedNode = dataContext.GetSelectedTreeNode<IExpression>();

            return cachedRegistrations.Any(r => r.RegistrationElement.Children().Contains(invokedNode));
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

            var invokedNode = dataContext.GetSelectedTreeNode<IExpression>();
            var registration = cachedRegistrations.FirstOrDefault(r => r.RegistrationElement.Children().Contains(invokedNode));
            if (registration == null)
            {
                return null;
            }

            return new RegisteredComponentsSearchRequest(solution, registration);
        }
    }
}
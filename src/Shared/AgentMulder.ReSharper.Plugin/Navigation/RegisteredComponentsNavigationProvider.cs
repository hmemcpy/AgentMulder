using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.ActionManagement;
using JetBrains.Application;
using JetBrains.Application.DataContext;
using JetBrains.DataFlow;
using JetBrains.ReSharper.Feature.Services;
using JetBrains.ReSharper.Feature.Services.ContextNavigation;
using JetBrains.ReSharper.Feature.Services.Search;
using JetBrains.ReSharper.Features.Common.Occurences.ExecutionHosting;
using JetBrains.ReSharper.Features.Finding.NavigateFromHere;
using JetBrains.TextControl;
using JetBrains.UI.Application;
using JetBrains.Util;

namespace AgentMulder.ReSharper.Plugin.Navigation
{
    [ContextNavigationProvider]
    public class RegisteredComponentsNavigationProvider : ContextNavigationProviderBase<IRegisteredComponentsContextSearch>, INavigateFromHereProvider
    {
        private readonly IShellLocks locks;
        private const string NoRegisteredTypesFound = "No components matching this registration were found";

        public RegisteredComponentsNavigationProvider(IFeaturePartsContainer manager, IShellLocks locks)
            : base(manager)
        {
            this.locks = locks;
        }

        protected override string GetActionId(IDataContext dataContext)
        {
            return "NavigateToRegisteredComponents";
        }

        protected override string GetNavigationMenuTitle(IDataContext dataContext)
        {
            return "Registered Components";
        }

        protected override void Execute(IDataContext dataContext, IEnumerable<IRegisteredComponentsContextSearch> searches, INavigationExecutionHost host)
        {
            List<RegisteredComponentsSearchRequest> requests = searches.SelectNotNull(item => item.GetRegisteredComponentsRequest(dataContext)).ToList();
            ITextControl textControl = dataContext.GetData(JetBrains.TextControl.DataContext.DataConstants.TEXT_CONTROL);
            if (textControl != null && requests.Any())
            {
                RegisteredComponentsSearchRequest requestToExecute = requests.First();
                ICollection<IOccurence> occurences = requestToExecute.Search();
                if (occurences.IsEmpty())
                {
                    Shell.Instance.Components.Tooltips().ShowAtCaret(EternalLifetime.Instance, NoRegisteredTypesFound, textControl, locks, Shell.Instance.GetComponent<IActionManager>());
                }
                else
                {
                    Func<SearchRegisteredComponentsDescriptor> descriptorBuilder = () => new SearchRegisteredComponentsDescriptor(requestToExecute, occurences);
#if !SDK80
                    host.ShowResultsPopupMenu(dataContext, occurences, descriptorBuilder, null, true, requestToExecute.Title);
#else
                    host.ShowContextPopupMenu(dataContext, occurences, descriptorBuilder, null, true, requestToExecute.Title);
#endif
                }
            }
        }

        protected override NavigationActionGroup ActionGroup
        {
            get { return NavigationActionGroup.Important; }
        }
    }
}
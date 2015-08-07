using JetBrains.Application.DataContext;
using JetBrains.ReSharper.Feature.Services.Navigation;

namespace AgentMulder.ReSharper.Plugin.Navigation
{
    public interface IRegisteredComponentsContextSearch : IContextSearch
    {
        RegisteredComponentsSearchRequest GetRegisteredComponentsRequest(IDataContext dataContext);
    }
}
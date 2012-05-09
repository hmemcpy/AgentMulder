using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using AgentMulder.Containers.Ninject.Patterns.Module.Bind;
using AgentMulder.ReSharper.Domain.Registrations;
using AgentMulder.ReSharper.Domain.Search;

namespace AgentMulder.Containers.Ninject.Providers
{
    [Export]
    [Export(typeof(IRegistrationPatternsProvider))]
    public class BindRegistrationProvider : IRegistrationPatternsProvider
    {
        private readonly ToRegistrationProvider toProvider;

        [ImportingConstructor]
        public BindRegistrationProvider(ToRegistrationProvider toProvider)
         {
             this.toProvider = toProvider;
         }

        public IEnumerable<RegistrationBasePattern> GetRegistrationPatterns()
        {
            var toPatterns = toProvider.GetRegistrationPatterns().ToArray();

            return new RegistrationBasePattern[]
            {
                new BindGeneric(toPatterns),
            };
        }
    }
}
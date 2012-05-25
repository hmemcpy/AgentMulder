using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using AgentMulder.Containers.Ninject.Patterns.Bind;
using AgentMulder.ReSharper.Domain.Patterns;
using AgentMulder.ReSharper.Domain.Registrations;

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

        public IEnumerable<IRegistrationPattern> GetRegistrationPatterns()
        {
            var toPatterns = toProvider.GetRegistrationPatterns().ToArray();

            return new IRegistrationPattern[]
            {
                new ModuleBindGeneric(toPatterns),
                new ModuleBindNonGeneric(toPatterns),
                new KernelBindGeneric(toPatterns),
                new KernelBindNonGeneric(toPatterns),
            };
        }
    }
}
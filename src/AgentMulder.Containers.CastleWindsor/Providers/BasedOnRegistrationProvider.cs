using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.BasedOn;
using AgentMulder.ReSharper.Domain.Patterns;
using AgentMulder.ReSharper.Domain.Registrations;

namespace AgentMulder.Containers.CastleWindsor.Providers
{
    [Export]
    public class BasedOnRegistrationProvider : IRegistrationPatternsProvider
    {
        private readonly List<IRegistrationPattern> whereArgumentPatterns = new List<IRegistrationPattern> 
        {
            
        };

        public IEnumerable<IBasedOnPattern> GetRegistrationPatterns()
        {
            return new IBasedOnPattern[]
            {
                new BasedOnGeneric(),
                new BasedOnNonGeneric(),
                new InNamespace(),
                new InSameNamespaceAsGeneric(),
                new InSameNamespaceAsNonGeneric(),
                new Pick(),
                new Where(whereArgumentPatterns),
            };
        }


        IEnumerable<IRegistrationPattern> IRegistrationPatternsProvider.GetRegistrationPatterns()
        {
            return GetRegistrationPatterns();
        }
    }
}
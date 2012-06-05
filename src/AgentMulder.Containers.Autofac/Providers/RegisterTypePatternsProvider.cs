using System.Collections.Generic;
using System.ComponentModel.Composition;
using AgentMulder.Containers.Autofac.Patterns;
using AgentMulder.ReSharper.Domain.Patterns;
using AgentMulder.ReSharper.Domain.Registrations;

namespace AgentMulder.Containers.Autofac.Providers
{
    [Export]
    [Export(typeof(IRegistrationPatternsProvider))]
    public class RegisterTypePatternsProvider : IRegistrationPatternsProvider
    {
        public IEnumerable<IRegistrationPattern> GetRegistrationPatterns()
        {
            return new IRegistrationPattern[]
            {
                new RegisterTypeGeneric(), 
            };
        }
    }
}
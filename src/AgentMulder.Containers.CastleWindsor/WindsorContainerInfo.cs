using System.Collections.Generic;
using System.ComponentModel.Composition;
using AgentMulder.Containers.CastleWindsor.Patterns;
using AgentMulder.Containers.CastleWindsor.Patterns.Component;
using AgentMulder.Containers.CastleWindsor.Patterns.FromTypes;
using AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.BasedOn;
using AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.WithService;
using AgentMulder.ReSharper.Domain.Containers;
using AgentMulder.ReSharper.Domain.Search;

namespace AgentMulder.Containers.CastleWindsor
{
    [Export(typeof(IContainerInfo))]
    public class WindsorContainerInfo : IContainerInfo
    {
        private readonly List<IRegistrationPattern> registrationPatterns;

        public IEnumerable<IRegistrationPattern> RegistrationPatterns
        {
            get { return registrationPatterns; }
        }

        public WindsorContainerInfo()
        {
            var implementedByPatterns = new ImplementedByBase[]
            {
                new ImplementedByGeneric(), new ImplementedByNonGeneric()
            };

            registrationPatterns = new List<IRegistrationPattern> 
            {
                new WindsorContainerRegisterPattern(
                            new ComponentForGeneric(implementedByPatterns),
                            new ComponentForNonGeneric(implementedByPatterns)),
                    
                    new AllTypesFrom(),

                    new AllTypesFromThisAssembly(
                        new BasedOnGeneric(
                            new WithServiceBase()))
            };
        }

        public string ContainerDisplayName
        {
            get { return "Castle Windsor"; }
        }
    }
}
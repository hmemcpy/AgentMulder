using System.Collections.Generic;
using System.ComponentModel.Composition;
using AgentMulder.Containers.CastleWindsor.Patterns;
using AgentMulder.Containers.CastleWindsor.Patterns.Component.ComponentFor;
using AgentMulder.Containers.CastleWindsor.Patterns.Component.ImplementedBy;
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
            var implementedByPatterns = new ImplementedByBasePattern[]
            {
                new ImplementedByGeneric(), new ImplementedByNonGeneric()
            };
            var withServices = new WithServiceRegistrationBasePattern[]
            {

            };
            var basedOnPatterns = new BasedOnRegistrationBasePattern[]
            {
                new BasedOnGeneric(withServices),
                new InNamespace(withServices),
                new InSameNamespaceAsGeneric(withServices), 
            };

            registrationPatterns = new List<IRegistrationPattern> 
            {
                new WindsorContainerRegisterPattern(
                            new ComponentForGeneric(implementedByPatterns),
                            new ComponentForNonGeneric(implementedByPatterns),
                    
                    new AllTypesFrom(),

                    new AllTypesFromAssembly(basedOnPatterns),

                    new AllTypesFromThisAssembly(basedOnPatterns))
            };
        }

        public string ContainerDisplayName
        {
            get { return "Castle Windsor"; }
        }
    }
}
using System.Collections.Generic;
using System.ComponentModel.Composition;
using AgentMulder.Containers.Autofac.Patterns;
using AgentMulder.Containers.Autofac.Patterns.FromAssemblies;
using AgentMulder.ReSharper.Domain.Containers;
using AgentMulder.ReSharper.Domain.Patterns;

namespace AgentMulder.Containers.Autofac
{
    [Export(typeof(IContainerInfo))]
    public class AutofacContainerInfo : IContainerInfo
    {
        private readonly List<IRegistrationPattern> registrationPatterns; 

        public string ContainerDisplayName
        {
            get { return "Autofac"; }
        }

        public IEnumerable<IRegistrationPattern> RegistrationPatterns
        {
            get { return registrationPatterns; }
        }

        public AutofacContainerInfo()
        {
            var basedOnPatterns = new IBasedOnPattern[]
            {
                new AsGeneric(),
                new AsNonGeneric(), 
            };

            registrationPatterns = new List<IRegistrationPattern> 
            {
                new RegisterTypeGeneric(),
                new RegisterTypeNonGeneric(),
                new RegisterLambdaExpression(),
                new RegisterLambdaStatements(),
                new RegisterAssemblyTypes(basedOnPatterns)
            };
        }
    }
}
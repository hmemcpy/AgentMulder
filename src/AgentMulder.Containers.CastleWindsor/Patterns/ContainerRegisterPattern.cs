using System;
using AgentMulder.Containers.CastleWindsor.Patterns.Component;
using AgentMulder.ReSharper.Domain.Search;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.Containers.CastleWindsor.Patterns
{
    internal sealed class ContainerRegisterPattern : RegistrationBase
    {
        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$container$.Register($arguments$)",
                new ExpressionPlaceholder("container", "Castle.Windsor.IWindsorContainer"),
                new ArgumentPlaceholder("arguments", -1, -1)); // any number of arguments

        private readonly IRegistrationPattern componentRegistration = new ComponentManualRegistration();

        public ContainerRegisterPattern()
            : base(pattern)
        {
        }

        public override IComponentRegistrationCreator CreateComponentRegistrationCreator()
        {
            return componentRegistration.CreateComponentRegistrationCreator();
        }
    }
}
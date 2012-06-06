using System.Collections.Generic;
using System.Reflection;
using AgentMulder.ReSharper.Domain.Patterns;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.Autofac.Patterns
{
    public class RegisterAssemblyTypes : RegistrationPatternBase 
    {
        public RegisterAssemblyTypes(IStructuralSearchPattern pattern)
            : base(pattern)
        {
        }

        public override IEnumerable<IComponentRegistration> GetComponentRegistrations(ITreeNode registrationRootElement)
        {
            throw new System.NotImplementedException();
        }
    }
}
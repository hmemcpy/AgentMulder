using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using AgentMulder.ReSharper.Domain.Patterns;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.StructureMap.Patterns
{
    [Export("ComponentRegistration", typeof(IRegistrationPattern))]
    internal sealed class ForGeneric : ComponentRegistrationPatternBase
    {
        private readonly ComponentImplementationPatternBase[] implementationPatterns;

        private static readonly IStructuralSearchPattern pattern = 
            new CSharpStructuralSearchPattern("$container$.For<$service$>()",
                new ExpressionPlaceholder("container", "global::StructureMap.ConfigurationExpression"),
                new TypePlaceholder("service"));

        [ImportingConstructor]
        public ForGeneric([ImportMany] params ComponentImplementationPatternBase[] implementationPatterns)
            : base(pattern, "service")
        {
            this.implementationPatterns = implementationPatterns;
        }

        public override IEnumerable<IComponentRegistration> GetComponentRegistrations(ITreeNode registrationRootElement)
        {
            // todo
            throw new NotImplementedException();
        }
    }
}
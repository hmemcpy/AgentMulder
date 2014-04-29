using System;
using System.Collections.Generic;
using AgentMulder.ReSharper.Domain.Patterns;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.BasedOn.WhereArgument
{
    internal class ComponentHasAttributeMethodGroup : BasedOnPatternBase
    {
        // note: there seems to be an issue with ReSharper SSR matching partial expessions (method groups)
        // so a fuller pattern can be matched by the root element
        private static readonly IStructuralSearchPattern pattern = 
            new CSharpStructuralSearchPattern("$expr$.Where($component$.HasAttribute<$attribute$>)",
                new ExpressionPlaceholder("expr"),
                new ExpressionPlaceholder("component", "Castle.MicroKernel.Registration.Component", true),
                new TypePlaceholder("attribute", "System.Attribute"));

        public ComponentHasAttributeMethodGroup()
            : base(pattern)
        {
        }

        public override IEnumerable<IComponentRegistration> GetComponentRegistrations(ITreeNode registrationRootElement)
        {
            return GetBasedOnRegistrations(registrationRootElement);
        }

        public override IEnumerable<FilteredRegistrationBase> GetBasedOnRegistrations(ITreeNode registrationRootElement)
        {
            IStructuralMatchResult match = Match(registrationRootElement);

            if (match.Matched)
            {
                var declaredType = match.GetMatchedType("attribute") as IDeclaredType;
                if (declaredType != null)
                {
                    var typeElement = declaredType.GetTypeElement();
                    if (typeElement != null)
                    {
                        yield return new HasAttributeRegistration(registrationRootElement, typeElement);
                    }
                }
            }
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using AgentMulder.ReSharper.Domain.Patterns;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.Autofac.Patterns.FromAssemblies.BasedOn
{
    internal sealed class ExceptGeneric : BasedOnPatternBase
    {
        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$builder$.Except<$type$>()",
                new ExpressionPlaceholder("builder",
                    "global::Autofac.Builder.IRegistrationBuilder<object,global::Autofac.Features.Scanning.ScanningActivatorData,global::Autofac.Builder.DynamicRegistrationStyle>", false),
                new TypePlaceholder("type"));

        public ExceptGeneric()
            : base(pattern)
        {
        }

        public override IEnumerable<IComponentRegistration> GetComponentRegistrations(ITreeNode registrationRootElement)
        {
            return GetBasedOnRegistrations(registrationRootElement);
        }

        public override IEnumerable<BasedOnRegistrationBase> GetBasedOnRegistrations(ITreeNode registrationRootElement)
        {
            foreach (IStructuralMatchResult match in MatchMany(registrationRootElement).Where(match => match.Matched))
            {
                var matchedType = match.GetMatchedType("type") as IDeclaredType;
                if (matchedType != null)
                {
                    ITypeElement typeElement = matchedType.GetTypeElement();
                    if (typeElement != null)
                    {
                        yield return new ExceptRegistration(registrationRootElement, typeElement);
                    }
                }
            }
        }

        private sealed class ExceptRegistration : BasedOnRegistrationBase
        {
            public ExceptRegistration(ITreeNode registrationRootElement, ITypeElement exceptElement)
                : base(registrationRootElement)
            {
                AddFilter(typeElement => !typeElement.Equals(exceptElement));
            }
        }
    }
}
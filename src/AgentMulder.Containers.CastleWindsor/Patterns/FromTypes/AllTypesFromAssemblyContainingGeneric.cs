using System;
using AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.BasedOn;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.Containers.CastleWindsor.Patterns.FromTypes
{
    internal sealed class AllTypesFromAssemblyContainingGeneric : ClassesFromAssemblyBasePattern
    {
        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$alltypes$.FromAssemblyContaining<$type$>()",
                                              new ExpressionPlaceholder("alltypes", "Castle.MicroKernel.Registration.AllTypes"),
                                              new TypePlaceholder("type"));

        public AllTypesFromAssemblyContainingGeneric(params BasedOnRegistrationBasePattern[] basedOnPatterns)
            : base(pattern, basedOnPatterns)
        {
        }

        protected override IModule GetTargetModule(IStructuralMatchResult match)
        {
            var declaredType = match.GetMatchedType("type") as IDeclaredType;
            if (declaredType == null)
            {
                return null;
            }

            var typeElement = declaredType.GetTypeElement();
            if (typeElement != null)
            {
                return typeElement.Module.ContainingProjectModule;
            }

            return null;
        }
    }
}
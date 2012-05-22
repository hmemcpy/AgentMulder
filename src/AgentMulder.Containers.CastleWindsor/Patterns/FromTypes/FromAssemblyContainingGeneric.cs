using System;
using AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.BasedOn;
using AgentMulder.ReSharper.Domain.Modules;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.Containers.CastleWindsor.Patterns.FromTypes
{
    internal sealed class FromAssemblyContainingGeneric : FromAssemblyBasePattern
    {
        public FromAssemblyContainingGeneric(string qualiferType, params BasedOnRegistrationBasePattern[] basedOnPatterns)
            : this(qualiferType, element => true, basedOnPatterns)
        {
        }

        public FromAssemblyContainingGeneric(string qualiferType, Predicate<ITypeElement> filter, params BasedOnRegistrationBasePattern[] basedOnPatterns)
            : base(CreatePattern(qualiferType), filter, basedOnPatterns)
        {
        }

        private static IStructuralSearchPattern CreatePattern(string qualiferType)
        {
            return new CSharpStructuralSearchPattern("$qualifier$.FromAssemblyContaining<$type$>()",
                new ExpressionPlaceholder("qualifier", qualiferType),
                new TypePlaceholder("type"));
        }

        protected override IModule GetTargetModule(IStructuralMatchResult match)
        {
            return ModuleExtractorProvider.GetTargetModule(match.GetMatchedType("type"));
        }
    }
}
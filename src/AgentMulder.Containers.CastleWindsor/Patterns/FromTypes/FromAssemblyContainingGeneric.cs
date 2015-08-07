using System;
using AgentMulder.ReSharper.Domain.Elements.Modules;
using AgentMulder.ReSharper.Domain.Patterns;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Feature.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Feature.Services.StructuralSearch;

namespace AgentMulder.Containers.CastleWindsor.Patterns.FromTypes
{
    internal sealed class FromAssemblyContainingGeneric : ModuleBasedPatternBase
    {
        public FromAssemblyContainingGeneric(string qualiferType)
            : base(CreatePattern(qualiferType))
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
            return ModuleExtractor.GetTargetModule(match.GetMatchedType("type"));
        }
    }
}
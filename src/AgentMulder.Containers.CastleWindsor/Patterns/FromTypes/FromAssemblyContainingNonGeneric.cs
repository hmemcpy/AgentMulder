using System;
using AgentMulder.ReSharper.Domain.Elements.Modules;
using AgentMulder.ReSharper.Domain.Patterns;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Feature.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Feature.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace AgentMulder.Containers.CastleWindsor.Patterns.FromTypes
{
    internal sealed class FromAssemblyContainingNonGeneric : ModuleBasedPatternBase
    {
        public FromAssemblyContainingNonGeneric(string qualiferType)
            : base(CreatePattern(qualiferType))
        {
        }

        private static IStructuralSearchPattern CreatePattern(string qualiferType)
        {
            return new CSharpStructuralSearchPattern("$qualifier$.FromAssemblyContaining($argument$)",
                new ExpressionPlaceholder("qualifier", qualiferType),
                new ArgumentPlaceholder("argument", 1, 1));
        }

        protected override IModule GetTargetModule(IStructuralMatchResult match)
        {
            var argument = (ICSharpArgument)match.GetMatchedElement("argument");

            return ModuleExtractor.GetTargetModule(argument.Value);
        }
    }
}
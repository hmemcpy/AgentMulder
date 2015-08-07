using System;
using AgentMulder.ReSharper.Domain.Elements.Modules;
using AgentMulder.ReSharper.Domain.Patterns;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Feature.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Feature.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Feature.Services.StructuralSearch;

namespace AgentMulder.Containers.CastleWindsor.Patterns.FromTypes
{
    internal sealed class FromAssembly : ModuleBasedPatternBase
    {
        public FromAssembly(string qualiferType)
            : base(CreatePattern(qualiferType))
        {
        }

        private static IStructuralSearchPattern CreatePattern(string qualiferType)
        {
            return new CSharpStructuralSearchPattern("$qualifier$.FromAssembly($assembly$)",
                new ExpressionPlaceholder("qualifier", qualiferType, true),
                new ArgumentPlaceholder("assembly", 1, 1)); // matches exactly one argument
        }

        protected override IModule GetTargetModule(IStructuralMatchResult match)
        {
            var argument = (ICSharpArgument)match.GetMatchedElement("assembly");

            return ModuleExtractor.GetTargetModule(argument.Value);
        }
    }
}
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
    internal sealed class FromAssemblyNamed : ModuleBasedPatternBase
    {
        public FromAssemblyNamed(string qualiferType)
            : base(CreatePattern(qualiferType))
        {
        }

        private static IStructuralSearchPattern CreatePattern(string qualiferType)
        {
            return new CSharpStructuralSearchPattern("$qualifer$.FromAssemblyNamed($assemblyName$)",
                new ExpressionPlaceholder("qualifer", qualiferType),
                new ArgumentPlaceholder("assemblyName", 1, 1)); // matches exactly one argume
        }

        protected override IModule GetTargetModule(IStructuralMatchResult match)
        {
            var argument = (ICSharpArgument)match.GetMatchedElement("assemblyName");
            
            return ModuleExtractor.GetTargetModule(argument.Value);
        }
    }
}
using System;
using AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.BasedOn;
using AgentMulder.ReSharper.Domain.Elements.Modules;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.Containers.CastleWindsor.Patterns.FromTypes
{
    internal sealed class FromAssemblyNamed : FromAssemblyBasePattern
    {
        public FromAssemblyNamed(string qualiferType, params IBasedOnPattern[] basedOnPatterns)
            : this(qualiferType, element => true, basedOnPatterns)
        {
        }

        public FromAssemblyNamed(string qualiferType, Predicate<ITypeElement> filter, params IBasedOnPattern[] basedOnPatterns)
            : base(CreatePattern(qualiferType), filter, basedOnPatterns)
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
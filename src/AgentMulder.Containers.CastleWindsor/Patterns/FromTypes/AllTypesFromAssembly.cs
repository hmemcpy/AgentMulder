using System;
using AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.BasedOn;
using AgentMulder.ReSharper.Domain.Modules;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.Containers.CastleWindsor.Patterns.FromTypes
{
    internal sealed class AllTypesFromAssembly : FromAssemblyBase
    {
        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$alltypes$.FromAssembly($assembly$)",
                new ExpressionPlaceholder("alltypes", "Castle.MicroKernel.Registration.AllTypes"),
                new ArgumentPlaceholder("assembly", 1, 1)); // matches exactly one argument

        public AllTypesFromAssembly(params BasedOnRegistrationBase[] basedOnPatterns)
            : base(pattern, basedOnPatterns)
        {
        }

        protected override IModule GetTargetModule(IStructuralMatchResult match)
        {
            var argument = (ICSharpArgument)match.GetMatchedElement("assembly");

            return ModuleExtractor.GetTargetModule(argument.Value);
        }
    }
}
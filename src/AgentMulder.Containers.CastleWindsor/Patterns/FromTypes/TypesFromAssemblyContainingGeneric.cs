using AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.BasedOn;
using AgentMulder.ReSharper.Domain.Modules;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.Containers.CastleWindsor.Patterns.FromTypes
{
    internal sealed class TypesFromAssemblyContainingGeneric : FromAssemblyBasePattern
    {
        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$alltypes$.FromAssemblyContaining<$type$>()",
                                              new ExpressionPlaceholder("alltypes", "Castle.MicroKernel.Registration.AllTypes"),
                                              new TypePlaceholder("type"));

        public TypesFromAssemblyContainingGeneric(params BasedOnRegistrationBasePattern[] basedOnPatterns)
            : base(pattern, basedOnPatterns)
        {
        }

        protected override IModule GetTargetModule(IStructuralMatchResult match)
        {
            return ModuleExtractor.Instance.GetTargetModule(match.GetMatchedType("type"));
        }
    }
}
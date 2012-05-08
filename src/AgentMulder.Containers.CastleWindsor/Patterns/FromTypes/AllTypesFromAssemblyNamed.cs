using System;
using System.Linq;
using AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.BasedOn;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.CastleWindsor.Patterns.FromTypes
{
    internal sealed class AllTypesFromAssemblyNamed : FromAssemblyBasePattern
    {
        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$alltypes$.FromAssemblyNamed($assemblyName$)",
                new ExpressionPlaceholder("alltypes", "Castle.MicroKernel.Registration.AllTypes"),
                new ArgumentPlaceholder("assemblyName", 1, 1)); // matches exactly one argume

        public AllTypesFromAssemblyNamed(params BasedOnRegistrationBasePattern[] basedOnPatterns)
            : base(pattern, basedOnPatterns)
        {
        }

        protected override IModule GetTargetModule(IStructuralMatchResult match)
        {
            var argument = match.GetMatchedElement("assemblyName") as ICSharpArgument;
            if (argument == null)
            {
                return null;
            }

            string assemblyName = Convert.ToString(argument.Value.ConstantValue.Value);

            ISolution solution = match.MatchedElement.GetSolution();

            IProject result = solution.GetAllProjects().FirstOrDefault(project => project.Name.Equals(assemblyName, StringComparison.OrdinalIgnoreCase));

            return result;
        }
    }
}
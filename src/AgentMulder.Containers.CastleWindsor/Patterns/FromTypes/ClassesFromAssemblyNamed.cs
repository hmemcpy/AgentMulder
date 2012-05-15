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
    internal sealed class ClassesFromAssemblyNamed : ClassesFromAssemblyBasePattern
    {
        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$classes$.FromAssemblyNamed($assemblyName$)",
                new ExpressionPlaceholder("classes", "Castle.MicroKernel.Registration.Classes"),
                new ArgumentPlaceholder("assemblyName", 1, 1)); // matches exactly one argument

        public ClassesFromAssemblyNamed(params BasedOnRegistrationBasePattern[] basedOnPatterns)
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

            if (argument.Value.ConstantValue == null ||
                !argument.Value.ConstantValue.IsString())
            {
                // currently only direct string values are supported;
                return null;
            }

            string assemblyName = Convert.ToString(argument.Value.ConstantValue.Value);

            ISolution solution = match.MatchedElement.GetSolution();

            IProject result = solution.GetAllProjects().FirstOrDefault(project => project.Name.Equals(assemblyName, StringComparison.OrdinalIgnoreCase));

            return result;
        }
    }
}
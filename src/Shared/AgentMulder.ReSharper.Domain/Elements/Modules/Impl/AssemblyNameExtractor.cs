using System;
using System.Linq;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.ReSharper.Domain.Elements.Modules.Impl
{
    internal class AssemblyNameExtractor : IModuleExtractor
    {
        public IModule GetTargetModule<T>(T element)
        {
            var value = element as ICSharpExpression;
            if (value == null ||
                value.ConstantValue == null ||
                !value.ConstantValue.IsString())
            {
                // currently only direct string values are supported;
                return null;
            }

            string assemblyName = Convert.ToString(value.ConstantValue.Value);

            ISolution solution = value.GetSolution();

            IProject result = solution.GetAllProjects().FirstOrDefault(project => project.Name.Equals(assemblyName, StringComparison.OrdinalIgnoreCase));

            return result;
        }

        IModule IElementExtractor<IModule>.ExtractElement<TElement>(TElement element)
        {
            return GetTargetModule(element);
        }
    }
}
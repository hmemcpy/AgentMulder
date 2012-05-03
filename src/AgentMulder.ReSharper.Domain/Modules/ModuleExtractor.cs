using System.Collections.Generic;
using System.Linq;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace AgentMulder.ReSharper.Domain.Modules
{
    public static class ModuleExtractor
    {
        private static readonly List<IModuleExtractor> extractors = new List<IModuleExtractor> 
        {
            new GetExecutingAssemblyExtractor(),
            new TypeOfExtractor(),
        };

        public static IModule GetTargetModule(ICSharpExpression expression)
        {
            return extractors.Select(extractor => extractor.GetTargetModule(expression)).FirstOrDefault(result => result != null);
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using AgentMulder.ReSharper.Domain.Modules.Impl;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.ReSharper.Domain.Modules
{
    public sealed class ModuleExtractor : IModuleExtractor
    {
        private class Nested
        {
            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
            static Nested() { }
            internal static readonly ModuleExtractor instance = new ModuleExtractor();
        }

        public static IModuleExtractor Instance
        {
            get { return Nested.instance; }
        }

        private static readonly List<IModuleExtractor> extractors = new List<IModuleExtractor>();

        static ModuleExtractor()
        {
            var typeElementExtractor = new TypeElementExtractor();
            var typeOfExtractor = new TypeOfExtractor(typeElementExtractor);
            var typeAssemblyPropertyExtractor = new TypeAssemblyPropertyExtractor(typeOfExtractor);
            
            extractors.Add(typeElementExtractor);
            extractors.Add(typeOfExtractor);
            extractors.Add(typeAssemblyPropertyExtractor);
            extractors.Add(new AssemblyNameExtractor());
            extractors.Add(new GetExecutingAssemblyExtractor());
        }

        private ModuleExtractor()
        {
        }

        public IModule GetTargetModule<T>(T element)
        {
            return extractors.Select(extractor => extractor.GetTargetModule(element)).FirstOrDefault(result => result != null);
        }
    }
}
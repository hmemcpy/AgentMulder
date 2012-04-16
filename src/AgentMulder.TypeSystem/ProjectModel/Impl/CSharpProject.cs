using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.CSharp.TypeSystem;
using ICSharpCode.NRefactory.TypeSystem;
using JetBrains.ProjectModel;
using JetBrains.ProjectModel.model2.Assemblies.ProjectToAssemblyResolvers;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using Mono.CSharp;
using CSharpParser = ICSharpCode.NRefactory.CSharp.CSharpParser;

namespace AgentMulder.TypeSystem.ProjectModel.Impl
{
    using JbIProject = JetBrains.ProjectModel.IProject;

    internal class CSharpProject : ICSharpProject
    {
        private readonly ISolution solution;
        private readonly JbIProject jbProject;
        private readonly List<IAssemblyReference> assemblyReferences = new List<IAssemblyReference>();
        private readonly ITypeResolveContext typeResolveContext;
        private readonly IProjectContent projectContent;

        public CSharpProject(ISolution solution, JbIProject jbProject)
        {
            this.solution = solution;
            this.jbProject = jbProject;

            projectContent = new CSharpProjectContent()
                .SetAssemblyName(jbProject.Name);

            var cecilLoader = new CecilLoader();
            var assembliesToResolve = new Lazy<IEnumerable<IUnresolvedAssembly>>(() =>
            {
                

                IProjectToAssemblyReference[] references = jbProject.GetAssemblyReferences().ToArray();
                var assemblies = new IUnresolvedAssembly[references.Length];
                Parallel.For(0, assemblies.Length, i =>
                {
                    assemblies[i] = cecilLoader.LoadAssemblyFile(references[i].)
                });
            });
            
            typeResolveContext = new CSharpTypeResolveContext(Compilation.MainAssembly);
        }

        public IEnumerable<IAssembly> AssemblyReferences
        {
            get { return projectContent.AssemblyReferences.Select(reference => reference.Resolve(typeResolveContext));  }
        }

        public ICompilation Compilation
        {
            get { return projectContent.CreateCompilation(solution.SolutionSnapshot); }
        }

        public ITypeResolveContext TypeResolveContext
        {
            get { return typeResolveContext; }
        }

        public string Name
        {
            get { return projectContent.AssemblyName; }
        }

        public CSharpParser CreateParser()
        {
            var settings = new CompilerSettings();

            return new CSharpParser(settings);
        }

        public IEnumerable<IFile> Files
        {
            get
            {
                if (jbProject.ProjectFile != null)
                {
                    return jbProject.ProjectFile
                        .EnumeratePsiFiles()
                        .OfType<ICSharpFile>()
                        .Select(file => new CSharpFile(file));
                }
                return Enumerable.Empty<IFile>();
            }
        }
    }
}
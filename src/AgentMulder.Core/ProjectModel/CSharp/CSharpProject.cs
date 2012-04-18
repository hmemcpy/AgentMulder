using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.CSharp.TypeSystem;
using ICSharpCode.NRefactory.TypeSystem;
using Microsoft.Build.Evaluation;

namespace AgentMulder.Core.ProjectModel.CSharp
{
    internal class CSharpProject : IProject
    {
        private readonly ITypeResolveContext context;

        public ISolution Solution { get; private set; }

        public IProjectContent ProjectContent { get; private set; }

        public string FileName { get; private set; }

        public string Title { get; private set; }

        public IEnumerable<ICodeFile> Files { get; private set; }

        public IEnumerable<IAssembly> ResolvedAssemblyReferences { get; private set; }

        public ICompilation Compilation
        {
            get { return Solution.SolutionSnapshot.GetCompilation(ProjectContent); }
        }

        public CSharpProject(ISolution solution, string title, string fileName)
        {
            Solution = solution;
            FileName = fileName;
            Title = title;

            var projectProperties = new ProjectProperties(fileName);
            
            Files = ParseFiles(projectProperties);
            var assemblyReferences = ParseReferences(projectProperties).ToList();

            ProjectContent = new CSharpProjectContent()
                .SetAssemblyName(projectProperties.AssemblyName)
                .AddAssemblyReferences(assemblyReferences)
                .UpdateProjectContent(null, Files.Select(file => file.CompilationUnit.ToTypeSystem()));

            context = new CSharpTypeResolveContext(Compilation.MainAssembly);
            ResolvedAssemblyReferences = assemblyReferences.Select(reference => reference.Resolve(context));
        }

        private IEnumerable<IAssemblyReference> ParseReferences(ProjectProperties projectProperties)
        {
            var referenceParser = new AssemblyReferencesParser(this, projectProperties);

            return referenceParser.GetAllAssemblyReferences();
        }

        private IEnumerable<CSharpFile> ParseFiles(ProjectProperties projectProperties)
        {
            return projectProperties.CompileItems.Select(compile => 
                new CSharpFile(this, Path.Combine(projectProperties.DirectoryPath, compile.EvaluatedInclude)));
        }

        private class ProjectProperties
        {
            public ProjectProperties(string fileName)
            {
                ProjectCollection.GlobalProjectCollection.UnloadAllProjects();
                var p = new Project(fileName);

                DirectoryPath = p.DirectoryPath;
                AssemblyName = p.GetPropertyValue("AssemblyName");
                AllowUnsafeBlocks = GetBoolProperty(p, "AllowUnsafeBlocks") ?? false;
                CheckForOverflowUnderflow = GetBoolProperty(p, "CheckForOverflowUnderflow") ?? false;
                PreprocessorDefines = p.GetPropertyValue("DefineConstants").Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                CompileItems = p.GetItems("Compile");
                ReferenceItems = p.GetItems("Reference");
                ProjectReferenceItems = p.GetItems("ProjectReference");
            }

            static bool? GetBoolProperty(Project p, string propertyName)
            {
                string val = p.GetPropertyValue(propertyName);
                if (val.Equals("true", StringComparison.OrdinalIgnoreCase))
                    return true;
                if (val.Equals("false", StringComparison.OrdinalIgnoreCase))
                    return false;
                return null;
            }

            public string DirectoryPath { get; private set; }
            public string AssemblyName { get; private set; }
            public bool AllowUnsafeBlocks { get; private set; }
            public bool CheckForOverflowUnderflow { get; private set; }
            public string[] PreprocessorDefines { get; private set; }
            public IEnumerable<ProjectItem> CompileItems { get; private set; }
            public IEnumerable<ProjectItem> ReferenceItems { get; private set; }
            public IEnumerable<ProjectItem> ProjectReferenceItems { get; private set; }
        }

        private class AssemblyReferencesParser
        {
            private readonly IProject project;
            private readonly ProjectProperties projectProperties;
            
            public AssemblyReferencesParser(IProject project, ProjectProperties projectProperties)
            {
                this.project = project;
                this.projectProperties = projectProperties;
            }

            public IEnumerable<IAssemblyReference> GetAllAssemblyReferences()
            {
                var unresolvedAssemblies = GetProjectAssembliesLocations().AsParallel().Select(location =>
                {
                    var loader = new CecilLoader();
                    return loader.LoadAssemblyFile(location);
                });
                var projectReferences = GetProjectReferences();

                return projectReferences.Concat(unresolvedAssemblies);
            }

            private IEnumerable<string> GetProjectAssembliesLocations()
            {
                yield return typeof(object).Assembly.Location; // mscorlib
                yield return typeof(Uri).Assembly.Location;    // System.dll
                yield return typeof(Enumerable).Assembly.Location; // System.Core.dll

                foreach (ProjectItem item in projectProperties.ReferenceItems)
                {
                    string assemblyFileName = null;
                    if (item.HasMetadata("HintPath"))
                    {
                        assemblyFileName = Path.Combine(projectProperties.DirectoryPath, item.GetMetadataValue("HintPath"));
                        if (!File.Exists(assemblyFileName))
                        {
                            continue;
                        }
                    }
                        
                    if (assemblyFileName != null)
                    {
                        if (Path.GetFileName(assemblyFileName).Equals("System.Core.dll", StringComparison.OrdinalIgnoreCase))
                        {
                            continue;
                        }
                        yield return assemblyFileName;
                    }
                }
            }

            private IEnumerable<IAssemblyReference> GetProjectReferences()
            {
                return projectProperties.ProjectReferenceItems.Select(item => new ProjectReference(project.Solution, item.GetMetadataValue("Name")));
            }
        }

        private class ProjectReference : IAssemblyReference
        {
            private readonly ISolution solution;
            private readonly string projectTitle;

            public ProjectReference(ISolution solution, string projectTitle)
            {
                this.solution = solution;
                this.projectTitle = projectTitle;
            }

            public IAssembly Resolve(ITypeResolveContext context)
            {
                var project = solution.Projects.Single(p => string.Equals(p.Title, projectTitle, StringComparison.OrdinalIgnoreCase));
                return project.ProjectContent.Resolve(context);
            }
        }
    }
}
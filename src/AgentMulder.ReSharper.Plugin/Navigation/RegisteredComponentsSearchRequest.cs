using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AgentMulder.ReSharper.Domain.Registrations;
using AgentMulder.ReSharper.Plugin.Components;
using JetBrains.Annotations;
using JetBrains.Application.Progress;
using JetBrains.ProjectModel;
using JetBrains.ProjectModel.Model2.Assemblies.Interfaces;
using JetBrains.ProjectModel.Model2.References;
using JetBrains.ReSharper.Feature.Services.Occurences;
using JetBrains.ReSharper.Feature.Services.ReferencedCode;
using JetBrains.ReSharper.Feature.Services.Search;
using JetBrains.ReSharper.Feature.Services.Search.SearchRequests;
using JetBrains.ReSharper.Features.Finding.FindDependentCode;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.Caches;
using JetBrains.ReSharper.Psi.ExtensionsAPI;
using JetBrains.ReSharper.Psi.Impl.Caches2.SymbolCache;
using JetBrains.ReSharper.Psi.Search;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Util;
using JetBrains.ReSharper.Psi;

namespace AgentMulder.ReSharper.Plugin.Navigation
{
    public class RegisteredComponentsSearchRequest : SearchRequest
    {
        private readonly ISolution solution;
        private readonly SolutionAnalyzer solutionAnalyzer;

        public RegisteredComponentsSearchRequest([NotNull] ISolution solution, SolutionAnalyzer solutionAnalyzer)
        {
            this.solution = solution;
            this.solutionAnalyzer = solutionAnalyzer;
        }

        public override ICollection<IOccurence> Search(IProgressIndicator progressIndicator)
        {
            ICollection<IProject> allProjects = solution.GetAllProjects();
            progressIndicator.Start(allProjects.Count);
            var typeElements = new List<ITypeDeclaration>();

            foreach (var project in allProjects)
            {
                var sourceFiles = project.GetAllProjectFiles().SelectMany(projectFile => projectFile.ToSourceFiles());
                foreach (var psiSourceFile in sourceFiles)
                {
                    IFile file = psiSourceFile.GetPsiFile(CSharpLanguage.Instance);
                    if (file == null)
                    {
                        continue;
                    }

                    file.ProcessChildren<ITypeDeclaration>(typeElements.Add);
                }
            }

            IEnumerable<IComponentRegistration> componentRegistrations = solutionAnalyzer.Analyze();
            
            return (from declaration in typeElements
                   where componentRegistrations.Any(registration => registration.IsSatisfiedBy(declaration.DeclaredElement))
                   select new DeclaredElementOccurence(declaration.DeclaredElement)).Cast<IOccurence>().ToList();
        }

        public override string Title
        {
            get { return "Foo!"; }
        }

        public override ISolution Solution
        {
            get { return solution; }
        }

        public override ICollection SearchTargets
        {
            get { return new ArrayList(); }
        }
    }
}
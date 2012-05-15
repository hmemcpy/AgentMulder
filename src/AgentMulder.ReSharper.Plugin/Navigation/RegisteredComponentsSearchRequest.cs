using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AgentMulder.ReSharper.Domain.Registrations;
using AgentMulder.ReSharper.Plugin.Components;
using JetBrains.Annotations;
using JetBrains.Application.Progress;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.Occurences;
using JetBrains.ReSharper.Feature.Services.Search;
using JetBrains.ReSharper.Feature.Services.Search.SearchRequests;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.Tree;
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
            
            return (typeElements.Where(declaration =>
                        componentRegistrations.Any(registration => registration.IsSatisfiedBy(declaration.DeclaredElement)))
                        .Select(declaration => new DeclaredElementOccurence(declaration.DeclaredElement))).Cast<IOccurence>().ToList();
        }

        public override string Title
        {
            get { return "Components registered via this registration"; }
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
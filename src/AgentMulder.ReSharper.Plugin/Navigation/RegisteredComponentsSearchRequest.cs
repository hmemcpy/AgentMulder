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
        private readonly IComponentRegistration componentRegistration;

        public RegisteredComponentsSearchRequest([NotNull] ISolution solution, IComponentRegistration componentRegistration)
        {
            this.solution = solution;
            this.componentRegistration = componentRegistration;
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

            return (from typeDeclaration in typeElements
                    let element = typeDeclaration.DeclaredElement
                    where element != null
                    where componentRegistration.IsSatisfiedBy(element)
                    select new DeclaredElementOccurence(element)).Cast<IOccurence>().ToList();
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
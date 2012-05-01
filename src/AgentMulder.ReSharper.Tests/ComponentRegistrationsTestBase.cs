using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AgentMulder.ReSharper.Domain;
using AgentMulder.ReSharper.Domain.Containers;
using AgentMulder.ReSharper.Domain.Registrations;
using AgentMulder.ReSharper.Domain.Search;
using JetBrains.Application.Components;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Search;
using JetBrains.ReSharper.TestFramework;
using JetBrains.Util;

namespace AgentMulder.ReSharper.Tests
{
    public abstract class ComponentRegistrationsTestBase : BaseTestWithSingleProject
    {
        protected abstract IContainerInfo ContainerInfo { get; }

        protected virtual string RelativeTypesPath
        {
            get { return "..\\Types"; }
        }

        protected void RunTest(string testName, Action<IEnumerable<IComponentRegistration>> action)
        {
            string fileName = testName + Extension;
            var dataPath = new DirectoryInfo(Path.Combine(SolutionItemsBasePath, RelativeTypesPath));
            var fileSet = dataPath.GetFiles("*.cs").SelectNotNull(fileInfo => Path.Combine(RelativeTypesPath, fileInfo.Name)).Concat(new[] { fileName });

            WithSingleProject(fileSet, (lifetime, project) => RunGuarded(() =>
            {
                var searchDomainFactory = ShellInstance.GetComponent<SearchDomainFactory>();
                var patternSearcher = new PatternSearcher(Solution, searchDomainFactory);
                var solutionnAnalyzer = new SolutionAnalyzer(patternSearcher, ContainerInfo);

                var componentRegistrations = solutionnAnalyzer.Analyze(Solution).ToList();

                action(componentRegistrations);
            }));
        }

        protected ICSharpFile GetCodeFile(string fileName)
        {
            PsiManager manager = PsiManager.GetInstance(Solution);
            IProjectFile projectFile = Project.GetAllProjectFiles(file => file.Name.Equals(fileName, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            if (projectFile != null)
            {
                return manager.GetPsiFile(projectFile.ToSourceFile(), CSharpLanguage.Instance) as ICSharpFile;
            }

            return null;
        }
    }
}
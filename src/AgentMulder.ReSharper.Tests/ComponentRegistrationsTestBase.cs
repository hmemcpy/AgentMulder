using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using AgentMulder.ReSharper.Domain.Containers;
using AgentMulder.ReSharper.Plugin.Components;
using JetBrains.Application;
using JetBrains.Application.Components;
using JetBrains.DocumentManagers;
using JetBrains.DocumentModel;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Search;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.TestFramework;
using JetBrains.Util;
using NUnit.Framework;
using FluentAssertions;

namespace AgentMulder.ReSharper.Tests
{
    [TestFixture]
    public abstract class ComponentRegistrationsTestBase : BaseTestWithSingleProject
    {
        private static readonly Regex patternCountRegex = new Regex(@"// Patterns: (?<patterns>\d+)");
        private static readonly Regex matchesRegex      = new Regex(@"// Matches: (?<files>.*?)\r?\n");
        private static readonly Regex notMatchesRegex   = new Regex(@"// NotMatches: (?<files>.*?)\r?\n");

        protected abstract IContainerInfo ContainerInfo { get; }

        protected virtual string RelativeTypesPath
        {
            get { return "..\\Types"; }
        }

        protected void RunTest(string fileName, Action<List<RegistrationInfo>> action)
        {
            fileName = Path.Combine(SolutionItemsBasePath, fileName);
            var typesPath = new DirectoryInfo(Path.Combine(SolutionItemsBasePath, RelativeTypesPath));
            var fileSet = typesPath.GetFiles("*" + Extension)
                                   .SelectNotNull(fileInfo => Path.Combine(RelativeTypesPath, fileInfo.Name))
                                   .Concat(new[] { fileName });

            WithSingleProject(fileSet, (lifetime, project) => RunGuarded(() =>
            {
                var documentManager = Solution.GetComponent<DocumentManager>();
                var patternSearcher = new PatternSearcher(documentManager);
                var searchDomainFactory = Shell.Instance.GetComponent<SearchDomainFactory>();
                var solutionAnalyzer = new SolutionAnalyzer(patternSearcher, Solution, searchDomainFactory);
                solutionAnalyzer.AddContainer(ContainerInfo);

                var componentRegistrations = solutionAnalyzer.Analyze();

                action(componentRegistrations.ToList());
            }));
        }

        protected ICSharpFile GetCodeFile(string fileName)
        {
            PsiManager manager = PsiManager.GetInstance(Solution);
            IProjectFile projectFile = Project.GetAllProjectFiles(file => file.Name.Equals(fileName, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            if (projectFile == null)
                return null;

            IPsiSourceFile psiSourceFile = projectFile.ToSourceFile();
            if (psiSourceFile == null)
                return null;

#if SDK70
            var cSharpFile = manager.GetPsiFile(psiSourceFile, CSharpLanguage.Instance, 
                new DocumentRange(psiSourceFile.Document, psiSourceFile.Document.DocumentRange)) as ICSharpFile;
#else
            var cSharpFile = manager.GetPsiFile(psiSourceFile, CSharpLanguage.Instance) as ICSharpFile;
#endif
            if (cSharpFile == null || string.IsNullOrEmpty(cSharpFile.GetText()))
            {
                Assert.Fail("Unable to open the file '{0}', or the file is empty", fileName);
            }

            return cSharpFile;
        }

// ReSharper disable MemberCanBePrivate.Global
        protected IEnumerable TestCases 
// ReSharper restore MemberCanBePrivate.Global
        {
            get
            {
                var testCasesDirectory = new DirectoryInfo(SolutionItemsBasePath);
                var testCases = testCasesDirectory.GetFiles("*" + Extension).Select(info => new TestCaseData(info.Name)).ToList();
                return testCases;
            }
        }

        [Test, TestCaseSource("TestCases")]
        public void Test(string fileName)
        {
            RunTest(fileName, registrations =>
            {
                ICSharpFile cSharpFile = GetCodeFile(fileName);
                var testData = GetTestData(cSharpFile);
                
                registrations.Count.Should().Be(testData.Item1, 
                    "Mismatched number of expected registrations. Make sure the '// Patterns:' comment is correct");

                IEnumerable<ICSharpFile> codeFiles = testData.Item2.SelectNotNull(GetCodeFile);
                foreach (ICSharpFile codeFile in codeFiles)
                {
                     codeFile.ProcessChildren<ITypeDeclaration>(declaration =>
                         registrations.Should().Contain(r => r.Registration.IsSatisfiedBy(declaration.DeclaredElement)));
                }
                codeFiles = testData.Item3.SelectNotNull(GetCodeFile);
                foreach (ICSharpFile codeFile in codeFiles)
                {
                     codeFile.ProcessChildren<ITypeDeclaration>(declaration =>
                         registrations.Should().NotContain(r => r.Registration.IsSatisfiedBy(declaration.DeclaredElement)));
                }
            });
        }

        private Tuple<int, string[], string[]> GetTestData(ICSharpFile cSharpFile)
        {
            string code = cSharpFile.GetText();
            var match = patternCountRegex.Match(code); 
            if (!match.Success)
            {
                Assert.Fail("Unable to find number of patterns. Make sure the '// Patterns:' comment is correct");
            }
            
            int count = Convert.ToInt32(match.Groups["patterns"].Value);

            match = matchesRegex.Match(code);
            if (!match.Success)
            {
                Assert.Fail("Unable to find matched files. Make sure the '// Matched:' comment is correct");
            }
            
            string[] matches = match.Groups["files"].Value.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);

            match = notMatchesRegex.Match(code);
            if (!match.Success)
            {
                Assert.Fail("Unable to find not-matched files. Make sure the '// NotMatched:' comment is correct");
            }
            string[] notMatches = match.Groups["files"].Value.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
            
            return Tuple.Create(count, matches, notMatches);
        }
    }
}
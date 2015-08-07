using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using AgentMulder.ReSharper.Domain.Containers;
using AgentMulder.ReSharper.Plugin.Components;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.TestFramework;
using JetBrains.Util;
using NUnit.Framework;
using AgentMulder.ReSharper.Domain.Utils;

#if SDK90
using JetBrains.TestFramework.Utils;
#endif

namespace AgentMulder.ReSharper.Tests
{
    [TestNetFramework45]
    [TestFileExtension(".cs")]
    public abstract class AgentMulderTestBase<TContainerInfo> : BaseTestWithSingleProject
        where TContainerInfo : IContainerInfo, new()
    {
        private static readonly Regex patternCountRegex = new Regex(@"// Patterns: (?<patterns>\d+)");
        private static readonly Regex matchesRegex      = new Regex(@"// Matches: (?<files>.*?)\r?\n");
        private static readonly Regex notMatchesRegex   = new Regex(@"// NotMatches: (?<files>.*?)\r?\n");

        protected virtual IContainerInfo ContainerInfo
        {
            get { return new TContainerInfo(); }
        }

        private void RunTest(string fileName, Action<IPatternManager> action)
        {
            var typesPath = new DirectoryInfo(Path.Combine(BaseTestDataPath.FullPath, "Types"));
            var fileSet = typesPath.GetFiles("*" + Extension)
                                   .SelectNotNull(fs => fs.FullName)
                                   .Concat(new[] { Path.Combine(SolutionItemsBasePath, fileName) });

            RunFixture(fileSet, () => { 
                var solutionAnalyzer = Solution.GetComponent<SolutionAnalyzer>();
                solutionAnalyzer.KnownContainers.Clear();
                solutionAnalyzer.KnownContainers.Add(ContainerInfo);

                var patternManager = Solution.GetComponent<IPatternManager>();

                action(patternManager);
            });
        }

        private void RunFixture(IEnumerable<string> fileSet, Action action)
        {
            WithSingleProject(fileSet, (lifetime, solution, project) => RunGuarded(action));
        }

        private ICSharpFile GetCodeFile(string fileName)
        {
            IProjectFile projectFile = Project.GetAllProjectFiles(file => file.Name.Equals(fileName, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            if (projectFile == null)
                return null;

            IPsiSourceFile psiSourceFile = projectFile.ToSourceFile();
            if (psiSourceFile == null)
                return null;

            ICSharpFile cSharpFile = psiSourceFile.GetCSharpFile();

            cSharpFile.AssertIsValid();
            
            return cSharpFile;
        }

// ReSharper disable MemberCanBePrivate.Global
        protected IEnumerable TestCases 
// ReSharper restore MemberCanBePrivate.Global
        {
            get
            {
#if SDK90
                TestUtil.SetHomeDir(GetType().Assembly);
#endif
                var testCasesDirectory = new DirectoryInfo(SolutionItemsBasePath);
                return testCasesDirectory.EnumerateFiles("*.cs").Select(info => new TestCaseData(info.Name)).ToList();
            }
        }

        [TestCaseSource("TestCases")]
        public void Test(string fileName)
        {
            RunTest(fileName, patternManager =>
            {
                ICSharpFile cSharpFile = GetCodeFile(fileName);
                var testData = GetTestData(cSharpFile);

                var patterns = patternManager.GetRegistrationsForFile(cSharpFile.GetSourceFile()).ToList();

                Assert.AreEqual(testData.Item1, patterns.Count, 
                    "Mismatched number of expected registrations. Make sure the '// Patterns:' comment is correct");

                if (testData.Item1 > 0)
                {
                    // todo refactor this. This should be a set operation.

                    // checks matching files
                    foreach (ICSharpFile codeFile in testData.Item2.SelectNotNull(GetCodeFile))
                    {
                        codeFile.ProcessChildren<ITypeDeclaration>(declaration =>
                        Assert.That(patterns.Any(r => r.Registration.IsSatisfiedBy(declaration.DeclaredElement)),
                                "Of {0} registrations, at least one should match '{1}'", patterns.Count, declaration.CLRName));
                    }

                    // checks non-matching files
                    foreach (ICSharpFile codeFile in testData.Item3.SelectNotNull(GetCodeFile))
                    {
                        codeFile.ProcessChildren<ITypeDeclaration>(declaration =>
                            Assert.That(patterns.All(r => !r.Registration.IsSatisfiedBy(declaration.DeclaredElement)),
                                "Of {0} registrations, none should match '{1}'", patterns.Count, declaration.CLRName));
                    }
                }
            });
        }

        private static Tuple<int, string[], string[]> GetTestData(ICSharpFile cSharpFile)
        {
            string code = cSharpFile.GetText();
            var match = patternCountRegex.Match(code); 
            if (!match.Success)
            {
                Assert.Fail("Unable to find number of patterns. Make sure the '// Patterns:' comment is correct");
            }
            
            int count = Convert.ToInt32(match.Groups["patterns"].Value);

            if (count == 0)
            {
                return Tuple.Create(0, EmptyArray<string>.Instance, EmptyArray<string>.Instance);
            }

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
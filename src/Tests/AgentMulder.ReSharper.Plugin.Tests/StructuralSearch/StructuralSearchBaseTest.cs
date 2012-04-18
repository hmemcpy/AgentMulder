using System.Collections.Generic;
using System.IO;
using System.Linq;
using AgentMulder.ReSharper.Plugin.Tests.StructuralSearch;
using AgentMulder.ReSharper.Plugin.Tests.StructuralSearch.Windsor;
using JetBrains.Application.Components;
using JetBrains.Application.Settings;
using JetBrains.DataFlow;
using JetBrains.DocumentManagers;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Features.Common.StructuralSearch;
using JetBrains.ReSharper.Features.StructuralSearch;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Search;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.TestFramework;
using JetBrains.ReSharper.TestFramework.Components.Features.StructuralSearch;
using JetBrains.Util;
using NUnit.Framework;

namespace JetBrains.ReSharper.StructuralSearchTests
{
    [Category("Structural Search")]
    public abstract class StructuralSearchBaseTest : BaseTestWithSingleProject
    {
        protected abstract IStructuralSearchPattern Pattern { get; }

        protected override void DoTest(IProject testProject)
        {
            var searchDomain = ShellInstance.GetComponent<SearchDomainFactory>().CreateSearchDomain(Solution, false);
            var engine = Solution.GetComponent<StructuralSearchEngine>();
            var documentManager = Solution.GetComponent<DocumentManager>();
            var request = new StructuralSearchRequest(testProject.GetSolution(), documentManager, searchDomain, engine, Pattern);
            var result = request.Search();

            ExecuteWithGold(TestName, w =>
            {
                PrintHeader(w);

                if (result != null)
                    foreach (var occurence in result.OrderBy(occurence => occurence.TextRange, TextRangeComparer.Default))
                        w.WriteLine(occurence.DumpToString());
            });
        }

        protected override string TestName
        {
            get { return TestContext.CurrentContext.Test.Name; }
        }

        protected virtual IEnumerable<IStructuralSearchPattern> GetPatterns()
        {
            yield return Pattern;
        }

        protected void DoHighlightingTest(IProject testProject)
        {
            var customPatternManager = Solution.GetComponent<TestCustomPatternManager>();
            IList<GuidIndex> patternIds = null;

            using (Disposable.CreateBracket(
              () =>
              {
                  patternIds = GetPatterns().Select(pattern => new CustomPattern { Pattern = pattern, Severity = Severity.HINT }).
                    Select(customPatternManager.AddPattern).ToList();
              },
              () =>
              {
                  foreach (var patternId in patternIds)
                  {
                      customPatternManager.RemovePattern(patternId);
                  }
              }
              ))
            {
                IProjectFolder folder = testProject;

                IProjectItem projectItem;

                while (true)
                {
                    projectItem = folder.GetSubItems().First();
                    if (!(projectItem is IProjectFolder))
                        break;
                    folder = projectItem as IProjectFolder;
                }

                var projectFile = GetProjectFile(projectItem.Name);
                var daemonProcess = new StructuralSearchTestDaemonProcess(projectFile.ToSourceFile());
                daemonProcess.DoHighlighting(DaemonProcessKind.VISIBLE_DOCUMENT);

                var testName = TestName.Split('.').Last();

                ExecuteWithGold(folder.Location.Combine(testName), w =>
                {
                    PrintHeader(w);
                    daemonProcess.Dump(w);
                });
            }
        }

        public override string ProjectBasePath { get { return SolutionItemsBasePath; } }
        protected virtual void PrintHeader(TextWriter output) { }
    }
}
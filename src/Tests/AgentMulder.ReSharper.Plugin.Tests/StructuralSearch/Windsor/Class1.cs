using System.Collections.Generic;
using System.IO;
using System.Linq;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Daemon.Test;
using JetBrains.ReSharper.Features.StructuralSearch.Highlightings;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.StructuralSearchTests.CSharp;
using JetBrains.Util;
using NUnit.Framework;

namespace AgentMulder.ReSharper.Plugin.Tests.StructuralSearch.Windsor
{
    public class WindsorSearchTests : CSharpStructuralSearchBaseTest
    {
        protected override string RelativeTestDataPath
        {
            get { return @"StructuralSearch\TestCases\Windsor"; }
        }

        [Test]
        public void Test01()
        {
            TestOnePattern("Component.For<$interface$>().ImplementedBy<$concrete$>()", new TypePlaceholder("interface"), new TypePlaceholder("concrete"));
        }
    }

    public class StructuralSearchTestDaemonProcess : TestDaemonProcess
    {
        private readonly List<HighlightingInfo> myHighlighters = new List<HighlightingInfo>();

        public StructuralSearchTestDaemonProcess(IPsiSourceFile sourceFile)
            : base(sourceFile)
        {
        }

        public override void CommitHighlighters(DaemonCommitContext context)
        {
            lock (this)
            {
                foreach (var highlighting in context.HighlightingsToAdd.Where(h => h.Highlighting is CustomPatternHighlighting))
                    myHighlighters.Add(highlighting);
                foreach (var highlighting in context.HighlightingsToRemove)
                    if (myHighlighters.Contains(highlighting))
                        myHighlighters.Remove(highlighting);
            }
        }

        public void Dump(TextWriter textWriter)
        {
            foreach (var highlightingInfo in CollectionUtil.Sort(myHighlighters.Distinct(info => info.Range.TextRange).ToList(), (a, b) => a.Range.TextRange.StartOffset.CompareTo(b.Range.TextRange.StartOffset)))
            {
                var range = highlightingInfo.Range;
                var coords = range.Document.GetCoordsByOffset(range.TextRange.StartOffset);
                var originalLine = range.Document.GetLineText(coords.Line);
                var line = originalLine.Replace('\t', ' ').TrimStart(' ');
                var endCoords = range.Document.GetCoordsByOffset(range.TextRange.EndOffset);
                var shift = originalLine.Length - line.Length;
                var end = endCoords.Line > coords.Line
                            ? ((int)range.Document.GetLineLength(coords.Line))
                            : (int)endCoords.Column;
                textWriter.WriteLine(string.Format("TO: [O] {0} RANGE: ({1},{2}) @ {3}",
                                                line.Insert((int)coords.Column - shift, "|").Insert(end - shift + 1, "|"),
                                                range.TextRange.StartOffset,
                                                range.TextRange.EndOffset,
                                                new FileSystemPath(range.Document.Moniker).Name));
            }
        }
    }
}
using System.Collections.Generic;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.TestFramework;

namespace JetBrains.ReSharper.StructuralSearchTests.CSharp
{
    [TestFileExtension(CSharpProjectFileType.CS_EXTENSION)]
    public abstract class CSharpStructuralSearchBaseTest : StructuralSearchBaseTest
    {
        private string mySearchString;
        private IEnumerable<IPlaceholder> myPlaceholders;
        private StructuralSearchPatternParams myParams;

        private static readonly StructuralSearchPatternParams ourStructuralSearchPatternDefaultParams = new StructuralSearchPatternParams { TreatReversedBinaryExpressionsEquivalent = TreatBinaryExpressionsEquivalent.Smart };
        private static readonly IPlaceholder[] ourDefaultPlaceholders = new IPlaceholder[] { new StatementPlaceholder("STMT"), new ExpressionPlaceholder("EXPR"), new IdentifierPlaceholder("REF"), new TypePlaceholder("TYPE") };

        protected override string RelativeTestDataPath
        {
            get { return @"StructuralSearch\CSharp"; }
        }

        protected override IStructuralSearchPattern Pattern
        {
            get { return new CSharpStructuralSearchPattern(mySearchString, myParams, myPlaceholders); }
        }

        protected void TestOnePattern(string pattern, string filename = null, StructuralSearchPatternParams @params = null, params IPlaceholder[] placeholders)
        {
            myPlaceholders = placeholders != null && placeholders.Length > 0 ? placeholders : ourDefaultPlaceholders;
            myParams = @params ?? ourStructuralSearchPatternDefaultParams;
            mySearchString = pattern;
            DoTestSolution(filename ?? TestMethodName + Extension);
        }

        protected void TestOnePattern(string pattern, string filename, params IPlaceholder[] placeholders)
        {
            TestOnePattern(pattern, filename, null, placeholders);
        }

        protected void TestOnePattern(string pattern, StructuralSearchPatternParams @params)
        {
            TestOnePattern(pattern, null, @params, null);
        }

        protected void TestOnePattern(string pattern, params IPlaceholder[] placeholders)
        {
            TestOnePattern(pattern, null, null, placeholders);
        }
    }
}
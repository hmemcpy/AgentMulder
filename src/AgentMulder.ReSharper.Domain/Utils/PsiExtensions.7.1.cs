using System;
using JetBrains.DocumentModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace AgentMulder.ReSharper.Domain.Utils
{
    public static partial class PsiExtensions
    {
        public static ICSharpFile GetCSharpFile(this IPsiSourceFile sourceFile)
        {
            return sourceFile.GetPsiFile<CSharpLanguage>(new DocumentRange(sourceFile.Document, sourceFile.Document.DocumentRange)) as ICSharpFile;
        }
    }
}
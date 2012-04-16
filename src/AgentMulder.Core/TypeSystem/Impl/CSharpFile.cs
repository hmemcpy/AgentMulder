using System;
using System.IO;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.Editor;

namespace AgentMulder.Core.TypeSystem.Impl
{
    internal class CSharpFile : IFile
    {
        public CSharpFile(IProject project, string fileName)
        {
            FileName = fileName;
            Content = new StringTextSource(File.ReadAllText(fileName));
            CompilationUnit = new CSharpParser().Parse(Content.CreateReader(), fileName);
        }

        public string FileName { get; private set; }

        public CompilationUnit CompilationUnit { get; private set; }

        public ITextSource Content { get; private set; }
    }
}
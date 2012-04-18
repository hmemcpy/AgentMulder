using System;
using System.IO;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.Editor;

namespace AgentMulder.Core.ProjectModel.CSharp
{
    internal class CSharpFile : ICodeFile
    {
        private readonly IProject project;

        public CSharpFile(IProject project, string fileName)
        {
            this.project = project;
            FileName = fileName;
            Content = new StringTextSource(File.ReadAllText(fileName));
            CompilationUnit = new CSharpParser().Parse(Content.CreateReader(), fileName);
        }

        public string FileName { get; private set; }

        public CompilationUnit CompilationUnit { get; private set; }

        public ITextSource Content { get; private set; }
    }
}
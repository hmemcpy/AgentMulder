using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace AgentMulder.TypeSystem.ProjectModel.Impl
{
    internal class CSharpFile : IFile
    {
        private readonly ICSharpFile file;

        public CSharpFile(ICSharpFile file)
        {
            this.file = file;
        }

        public string GetText()
        {
            return file.GetText();
        }
    }
}
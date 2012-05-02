using JetBrains.ReSharper.Psi.Search;

namespace AgentMulder.ReSharper.Domain.Search
{
    public class TypeDeclarationsDomainVisitor : SearchDomainVisitor
    {
        public override bool ProcessingIsFinished
        {
            get { return false; }
        }
    }
}
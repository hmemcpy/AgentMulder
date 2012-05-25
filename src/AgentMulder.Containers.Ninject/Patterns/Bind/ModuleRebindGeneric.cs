using AgentMulder.ReSharper.Domain.Patterns;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.Containers.Ninject.Patterns.Bind
{
    internal sealed class ModuleRebindGeneric : BindBasePattern
    {
        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("Rebind<$service$>()",
                new TypePlaceholder("service"));

        public ModuleRebindGeneric(params ComponentImplementationPatternBase[] toPatterns)
            : base(pattern, "service", toPatterns)
        {
        }

        protected override string GetXmlDocIdName(IMethod method)
        {
            return string.Format("M:Ninject.Syntax.BindingRoot.Rebind``{0}", method.TypeParameters.Count);
        }
    }
}
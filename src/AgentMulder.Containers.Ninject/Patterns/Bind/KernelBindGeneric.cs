using AgentMulder.ReSharper.Domain.Patterns;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.Containers.Ninject.Patterns.Bind
{
    internal sealed class KernelBindGeneric : BindBasePattern
    {
        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$kernel$.Bind<$service$>()",
                new ExpressionPlaceholder("kernel", "global::Ninject.Syntax.BindingRoot", false),
                new TypePlaceholder("service"));

        public KernelBindGeneric(params ComponentImplementationPatternBase[] toPatterns)
            : base(pattern, "service", toPatterns)
        {
        }

        protected override string GetXmlDocIdName(IMethod method)
        {
            return string.Format("M:Ninject.Syntax.BindingRoot.Bind``{0}", method.TypeParameters.Count);
        }
    }
}
﻿using AgentMulder.ReSharper.Domain.Patterns;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.Containers.Ninject.Patterns.Bind
{
    internal sealed class ModuleBindGeneric : BindBasePattern
    {
        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("Bind<$service$>()", 
                new TypePlaceholder("service"));

        public ModuleBindGeneric(params ComponentImplementationPatternBase[] toPatterns)
            : base(pattern, "service", toPatterns)
        {
        }

        protected override string GetXmlDocIdName(IMethod method)
        {
            return string.Format("M:Ninject.Syntax.BindingRoot.Bind``{0}", method.TypeParameters.Count);
        }
    }
}
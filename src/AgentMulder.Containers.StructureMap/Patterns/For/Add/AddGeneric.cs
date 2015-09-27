using System.ComponentModel.Composition;
using AgentMulder.ReSharper.Domain.Patterns;
using JetBrains.ReSharper.Feature.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Feature.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Feature.Services.StructuralSearch;

namespace AgentMulder.Containers.StructureMap.Patterns.For.Add
{
    [Export(typeof(ComponentImplementationPatternBase))]
    internal sealed class AddGeneric : ComponentImplementationPatternBase
    {
        private static readonly IStructuralSearchPattern pattern = 
            new CSharpStructuralSearchPattern("$createExpr$.Add<$type$>()",
                new ExpressionPlaceholder("createExpr", "global::StructureMap.Configuration.DSL.Expressions.CreatePluginFamilyExpression<...>"),
                new TypePlaceholder("type"));

        public AddGeneric()
            : base(pattern, "type")
        {
        }
    }
}
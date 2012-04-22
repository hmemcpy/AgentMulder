using System;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.Containers.CastleWindsor.Patterns.FromTypes
{
    internal sealed class AllTypesFrom : FromTypesBase
    {
        private static readonly IStructuralSearchPattern pattern = 
            new CSharpStructuralSearchPattern("$alltypes$.From($services$)",
                new ExpressionPlaceholder("alltypes", "Castle.MicroKernel.Registration.AllTypes"),
                new ArgumentPlaceholder("services"));

        public AllTypesFrom()
            : base(pattern, "services")
        {
        }
    }
}
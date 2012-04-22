using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.Containers.CastleWindsor.Patterns.FromTypes
{
    internal sealed class ClassesFrom : FromTypesBase
    {
        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$classes$.From($services$)",
                new ExpressionPlaceholder("classes", "Castle.MicroKernel.Registration.Classes", true),
                new ArgumentPlaceholder("services"));

        public ClassesFrom()
            : base(pattern, "services")
        {
        }
    }
}
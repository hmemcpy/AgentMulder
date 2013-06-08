using System.ComponentModel.Composition;
using AgentMulder.ReSharper.Domain.Patterns;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.Containers.SimpleInjector.Patterns
{
    [Export("ComponentRegistration", typeof(IRegistrationPattern))]
    public class LifestyleCreateRegistration : RegisterWithService
    {
        // Lifestyle instance takes the Container as the parameter (or the last parameter in the non-generic version)
        // bug: for some reason, ReSharper cannot match this in 7.1 (works in 6.1)
        // bug: pending investigation by @kropp http://youtrack.jetbrains.com/issue/RSRP-367240
        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$lifestyle$.CreateRegistration($arguments$)",
                new ExpressionPlaceholder("lifestyle", "global::SimpleInjector.Lifestyle", false),
                new ArgumentPlaceholder("arguments", -1, -1));

        public LifestyleCreateRegistration()
            : base(pattern)
        {
        }
    }
}
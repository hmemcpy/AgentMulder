using System.ComponentModel.Composition;
using AgentMulder.ReSharper.Domain.Patterns;
using JetBrains.ReSharper.Feature.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Feature.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Feature.Services.StructuralSearch;

namespace AgentMulder.Containers.SimpleInjector.Patterns
{
    [Export("ComponentRegistration", typeof(IRegistrationPattern))]
    public class LifestyleCreateRegistration : RegisterWithService
    {
        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$lifestyle$.CreateRegistration($arguments$)",
                new ExpressionPlaceholder("lifestyle", "global::SimpleInjector.Lifestyle"),
                new ArgumentPlaceholder("arguments"));

        public LifestyleCreateRegistration()
            : base(pattern)
        {
        }
    }
}
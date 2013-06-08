using System.ComponentModel.Composition;
using AgentMulder.ReSharper.Domain.Patterns;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.Containers.Unity.Patterns
{
    [Export("ComponentRegistration", typeof(IRegistrationPattern))]
    public class UnityRegisterWithService : RegisterWithService
    {
        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$container$.RegisterType($arguments$)",
            // ReSharper disable RedundantArgumentDefaultValue
            new ExpressionPlaceholder("container", "Microsoft.Practices.Unity.IUnityContainer", false),
            // ReSharper restore RedundantArgumentDefaultValue
            new ArgumentPlaceholder("arguments", -1, -1));

        public UnityRegisterWithService()
            : base(pattern)
        {
        }
    }
}
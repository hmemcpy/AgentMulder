using System.Collections.Generic;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.CastleWindsor.Patterns.Component
{
    internal class ComponentForGeneric : ManualRegistrationBase
    {
        private readonly ManualRegistrationBase[] manualRegistrationPatterns;

        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$component$.For<$service$>()",
                                              new ExpressionPlaceholder("component", "Castle.MicroKernel.Registration.Component"),
                                              new TypePlaceholder("service"));

        public ComponentForGeneric(params ManualRegistrationBase[] manualRegistrationPatterns)
            : base(pattern, "service")
        {
            this.manualRegistrationPatterns = manualRegistrationPatterns;
        }

        public override IEnumerable<IComponentRegistration> GetComponentRegistrations(ITreeNode parentElement)
        {
            bool innerMatch = false;
            foreach (var innerPattern in manualRegistrationPatterns)
            {
                foreach (var registration in innerPattern.GetComponentRegistrations(parentElement))
                {
                    innerMatch = true;
                    yield return registration;
                }
            }
            if (!innerMatch)
            {
                IInvocationExpression invocationExpression = GetMatchedExpression(parentElement);

                foreach (var registration in base.GetComponentRegistrations(invocationExpression))
                {
                    yield return registration;
                }
            }
        }
    }
}
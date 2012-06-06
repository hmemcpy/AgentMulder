using System;
using System.Collections.Generic;
using System.Linq;
using AgentMulder.ReSharper.Domain.Patterns;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.BasedOn
{
    internal sealed class Where : RegistrationPatternBase, IBasedOnPattern
    {
        private readonly IEnumerable<IBasedOnPattern> whereArgumentPatterns;

        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$fromDescriptor$.Where($argument$)",
                new ExpressionPlaceholder("fromDescriptor", "Castle.MicroKernel.Registration.FromDescriptor", false),
                new ExpressionPlaceholder("argument"));

        public Where(IEnumerable<IBasedOnPattern> whereArgumentPatterns)
            : base(pattern)
        {
            this.whereArgumentPatterns = whereArgumentPatterns;
        }

        public override IEnumerable<IComponentRegistration> GetComponentRegistrations(ITreeNode registrationRootElement)
        {
            return ((IBasedOnPattern)this).GetComponentRegistrations(registrationRootElement);
        }

        IEnumerable<BasedOnRegistrationBase> IBasedOnPattern.GetComponentRegistrations(ITreeNode registrationRootElement)
        {
            IStructuralMatchResult match = Match(registrationRootElement);

            if (match.Matched)
            {
                ITreeNode element = match.GetMatchedElement("argument");
                if (element != null)
                {
                    foreach (var whereArgumentPattern in whereArgumentPatterns)
                    {
                        var registrations = whereArgumentPattern.GetComponentRegistrations(element).ToArray();
                        if (!registrations.Any())
                        {
                            // try with the root element.
                            registrations = whereArgumentPattern.GetComponentRegistrations(registrationRootElement).ToArray();
                        }

                        foreach (var registration in registrations)
                        {
                            yield return registration;
                        }
                    }
                }
            }
        }
    }
}
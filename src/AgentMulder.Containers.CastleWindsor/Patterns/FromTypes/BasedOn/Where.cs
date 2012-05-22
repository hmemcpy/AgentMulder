using System;
using System.Collections.Generic;
using System.Linq;
using AgentMulder.ReSharper.Domain.Patterns;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.StructuralSearch.Impl;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.BasedOn
{
    internal sealed class Where : RegistrationPatternBase, IBasedOnPattern
    {
        private readonly IEnumerable<IRegistrationPattern> whereArgumentPatterns;

        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$fromDescriptor$.Where($argument$)",
                new ExpressionPlaceholder("fromDescriptor", "Castle.MicroKernel.Registration.FromDescriptor", false),
                new ExpressionPlaceholder("argument"));
        
        public Where(IEnumerable<IRegistrationPattern> whereArgumentPatterns)
            : base(pattern)
        {
            this.whereArgumentPatterns = whereArgumentPatterns;
        }

        public override IEnumerable<IComponentRegistration> GetComponentRegistrations(ITreeNode parentElement)
        {
            IStructuralMatchResult match = Match(parentElement);

            if (match.Matched)
            {
                ITreeNode element = match.GetMatchedElement("argument");
                if (element != null)
                {
                    var whereArgumentPattern = whereArgumentPatterns.FirstOrDefault(p => p.Matcher.Match(parentElement) != StructuralMatchResult.NOT_MATCHED);
                    if (whereArgumentPattern != null)
                    {
                        foreach (var registration in whereArgumentPattern.GetComponentRegistrations(parentElement))
                        {
                            yield return registration;
                        }
                    }
                }
            }
        }
    }
}
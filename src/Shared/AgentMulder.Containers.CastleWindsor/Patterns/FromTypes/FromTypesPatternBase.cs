using System;
using System.Collections.Generic;
using System.Linq;
using AgentMulder.ReSharper.Domain.Patterns;
using AgentMulder.ReSharper.Domain.Registrations;
using AgentMulder.ReSharper.Domain.Utils;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.CastleWindsor.Patterns.FromTypes
{
    public abstract class FromTypesPatternBase : RegistrationPatternBase
    {
        private readonly IEnumerable<IBasedOnPattern> basedOnPatterns;

        protected FromTypesPatternBase(IStructuralSearchPattern pattern, IEnumerable<IBasedOnPattern> basedOnPatterns)
            : base(pattern)
        {
            this.basedOnPatterns = basedOnPatterns;
        }

        public override IEnumerable<IComponentRegistration> GetComponentRegistrations(ITreeNode registrationRootElement)
        {
            IStructuralMatchResult match = Match(registrationRootElement);

            if (match.Matched)
            {
                var registrations = (from p in basedOnPatterns
                                     from registration in p.GetBasedOnRegistrations(registrationRootElement)
                                     select registration).ToList();

                if (registrations.Any())
                {
                    foreach (FilteredRegistrationBase basedOnRegistration in registrations)
                    {
                        IEnumerable<ICSharpArgument> matchedArguments = match.GetMatchedElementList("services").OfType<ICSharpArgument>();
                        IEnumerable<ITypeElement> typeElements = matchedArguments.SelectMany(argument => argument.Value.GetRegisteredTypes());

                        yield return new TypesBasedOnRegistration(typeElements, basedOnRegistration);
                    }
                }
            }
        }
    }
}
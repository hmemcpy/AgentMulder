using System;
using System.Collections.Generic;
using System.Linq;
using AgentMulder.ReSharper.Domain.Registrations;
using AgentMulder.ReSharper.Domain.Search;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.Ninject.Patterns
{
    public class NinjectModulePattern : RegistrationBasePattern
    {
        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("class $module$ : NinjectModule { public override void Load() { $statements$ } }",
                new IdentifierPlaceholder("module"),
                new StatementPlaceholder("statements", -1, -1));

        public NinjectModulePattern(RegistrationBasePattern[] bindPatterns)
            : base(pattern)
        {
            
        }

        public override IEnumerable<IComponentRegistration> GetComponentRegistrations(ITreeNode registrationRootElement)
        {
            IStructuralMatchResult match = Match(registrationRootElement);

            if (match.Matched)
            {
                IEnumerable<ICSharpStatement> statements = match.GetMatchedElementList("statements").Cast<ICSharpStatement>();

            }

            yield break;
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.CastleWindsor.Patterns.Component
{
    internal abstract class ComponentForBase : ComponentRegistrationBase
    {
        private readonly IEnumerable<ImplementedByBase> implementedByPatterns;

        protected ComponentForBase(IStructuralSearchPattern pattern, string elementName, IEnumerable<ImplementedByBase> implementedByPatterns) 
            : base(pattern, elementName)
        {
            this.implementedByPatterns = implementedByPatterns;
        }

        public override IEnumerable<IComponentRegistration> GetComponentRegistrations(ITreeNode parentElement)
        {
            IStructuralMatchResult match = Match(parentElement);
            
            if (match.Matched)
            {
                foreach (var implementedByPattern in implementedByPatterns)
                {
                    var implementedByRegistrations = implementedByPattern.GetComponentRegistrations(parentElement).ToList();
                    if (implementedByRegistrations.Any())
                    {
                        return implementedByRegistrations;
                    }
                }
            }

            return base.GetComponentRegistrations(parentElement);
        }
    }
}
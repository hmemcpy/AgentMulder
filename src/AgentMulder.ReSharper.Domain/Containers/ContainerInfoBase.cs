using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Reflection;
using AgentMulder.ReSharper.Domain.Patterns;

namespace AgentMulder.ReSharper.Domain.Containers
{
    [Export(typeof(IContainerInfo))]
    public abstract class ContainerInfoBase : IContainerInfo
    {
        public abstract string ContainerDisplayName { get; }

        [ImportMany("ComponentRegistration", typeof(IRegistrationPattern))]
        public IEnumerable<IRegistrationPattern> RegistrationPatterns { get; private set; }

        protected ContainerInfoBase()
        {
            ComposeParts();
        }

        private void ComposeParts()
        {
            var catalog = GetComponentCatalog();
            var container = new CompositionContainer(catalog);
            container.ComposeParts(this);
        }

        protected abstract ComposablePartCatalog GetComponentCatalog();
    }
}
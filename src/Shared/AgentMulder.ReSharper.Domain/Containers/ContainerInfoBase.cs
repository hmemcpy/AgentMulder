using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using AgentMulder.ReSharper.Domain.Patterns;

namespace AgentMulder.ReSharper.Domain.Containers
{
    [InheritedExport(typeof(IContainerInfo))]
    public abstract class ContainerInfoBase : IContainerInfo
    {
        public abstract string ContainerDisplayName { get; }
        public IEnumerable<IRegistrationPattern> RegistrationPatterns { get; private set; }
        public abstract IEnumerable<string> ContainerQualifiedNames { get; }

        protected ContainerInfoBase()
        {
            ComposeParts();
        }

        private void ComposeParts()
        {
            var catalog = GetComponentCatalog();
            var container = new CompositionContainer(catalog);
            RegistrationPatterns = container.GetExportedValues<IRegistrationPattern>("ComponentRegistration");
        }

        protected abstract ComposablePartCatalog GetComponentCatalog();
    }
}
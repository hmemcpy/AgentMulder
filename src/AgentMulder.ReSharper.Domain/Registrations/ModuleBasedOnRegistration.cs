using System.Collections.Generic;
using System.Linq;
using JetBrains.DocumentModel;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;

namespace AgentMulder.ReSharper.Domain.Registrations
{
    public class TypesBasedOnRegistration : BasedOnRegistration
    {
        private readonly IEnumerable<ITypeElement> types;

        public TypesBasedOnRegistration(IEnumerable<ITypeElement> types, BasedOnRegistration basedOn)
            : base(basedOn.DocumentRange, basedOn.BasedOnElement, basedOn.WithServices)
        {
            this.types = types.ToArray();
        }

        public override bool IsSatisfiedBy(ITypeElement typeElement)
        {
            if (!types.Contains(typeElement))
                return false;

            return base.IsSatisfiedBy(typeElement);
        }

        public override string ToString()
        {
            return string.Format("From types: {0}, {1}",
                string.Join(", ", types.Select(registration => registration.ToString()), base.ToString()));
        }
    }

    public class ModuleBasedOnRegistration : BasedOnRegistration
    {
        private readonly IModule sourceModule;

        public ModuleBasedOnRegistration(IModule sourceModule, BasedOnRegistration basedOn)
            : base(basedOn.DocumentRange, basedOn.BasedOnElement, basedOn.WithServices)
        {
            this.sourceModule = sourceModule;
        }

        public override bool IsSatisfiedBy(ITypeElement typeElement)
        {
            IModule targetModule = typeElement.Module.ContainingProjectModule;
            if (targetModule == null)
                return false;

            return (targetModule.Equals(sourceModule) && base.IsSatisfiedBy(typeElement));
        }

        public override string ToString()
        {
            return string.Format("In module: {0}, {1}", sourceModule.Name, base.ToString());
        }
    }
}
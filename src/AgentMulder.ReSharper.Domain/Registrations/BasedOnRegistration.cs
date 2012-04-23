using System.Collections.Generic;
using System.Linq;
using JetBrains.DocumentModel;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;

namespace AgentMulder.ReSharper.Domain.Registrations
{
    public class ModuleBasedOnRegistration : BasedOnRegistration
    {
        private readonly IModule sourceModule;

        public ModuleBasedOnRegistration(IModule sourceModule, BasedOnRegistration basedOn)
            : base(basedOn.DocumentRange, basedOn.BasedOnElement)
        {
            this.sourceModule = sourceModule;
        }

        public override bool IsSatisfiedBy(ITypeElement typeElement)
        {
            IModule targetModule = typeElement.GetSourceFiles().First().PsiModule.ContainingProjectModule;
            if (targetModule == null)
                return false;

            return (targetModule.Equals(sourceModule) && base.IsSatisfiedBy(typeElement));
        }
    }

    public class BasedOnRegistration : ComponentRegistrationBase
    {
        private readonly ITypeElement basedOnElement;
        private readonly string name;

        public ITypeElement BasedOnElement
        {
            get { return basedOnElement; }
        }

        public BasedOnRegistration(DocumentRange documentRange, ITypeElement basedOnElement)
            : base(documentRange)
        {
            this.basedOnElement = basedOnElement;
            name = basedOnElement.GetClrName().FullName;
        }

        public override bool IsSatisfiedBy(ITypeElement typeElement)
        {
            IList<IDeclaredType> superTypes = basedOnElement.GetSuperTypes();
            foreach (var superType in superTypes)
            {
                // todo horrible hack
                if (superType.GetClrName().FullName == typeElement.GetClrName().FullName)
                {
                    return true;
                }
            }
            return false;
        }

        public override string ToString()
        {
            return string.Format("Based on: {0}", name);
        }
    }
}
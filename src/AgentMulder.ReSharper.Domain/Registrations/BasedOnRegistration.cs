using System.Collections.Generic;
using JetBrains.DocumentModel;
using JetBrains.ReSharper.Psi;

namespace AgentMulder.ReSharper.Domain.Registrations
{
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
            IList<IDeclaredType> baseTypes = typeElement.GetSuperTypes();
            foreach (var baseType in baseTypes)
            {
                // todo horrible hack
                if (baseType.GetClrName().FullName == basedOnElement.GetClrName().FullName)
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
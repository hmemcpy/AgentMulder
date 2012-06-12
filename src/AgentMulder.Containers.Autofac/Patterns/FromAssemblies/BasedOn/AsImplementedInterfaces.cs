using System.Collections.Generic;
using System.Linq;
using AgentMulder.ReSharper.Domain.Patterns;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Util;

namespace AgentMulder.Containers.Autofac.Patterns.FromAssemblies.BasedOn
{
    public class AsImplementedInterfaces : BasedOnPatternBase
    {
        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$builder$.AsImplementedInterfaces()",
                new ExpressionPlaceholder("builder",
                    "global::Autofac.Builder.IRegistrationBuilder<object,global::Autofac.Features.Scanning.ScanningActivatorData,global::Autofac.Builder.DynamicRegistrationStyle>", false));

        public AsImplementedInterfaces()
            : base(pattern)
        {
        }

        public override IEnumerable<IComponentRegistration> GetComponentRegistrations(ITreeNode registrationRootElement)
        {
            return GetBasedOnRegistrations(registrationRootElement);
        }

        public override IEnumerable<BasedOnRegistrationBase> GetBasedOnRegistrations(ITreeNode registrationRootElement)
        {
            IStructuralMatchResult match = Match(registrationRootElement);

            if (match.Matched)
            {
                yield return new ImplementedInterfacesRegistration(registrationRootElement);
            }
        }

        private class ImplementedInterfacesRegistration : BasedOnRegistrationBase
        {
            public ImplementedInterfacesRegistration(ITreeNode registrationRootElement)
                : base(registrationRootElement)
            {
                AddFilter(typeElement => typeElement.GetSuperTypes().SelectNotNull(type => type.GetTypeElement() as IInterface)
                                            .Any(@interface => @interface.GetClrName().FullName != "System.IDisposable"));
            }
        }
    }
}
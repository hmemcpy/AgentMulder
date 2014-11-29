using System.Collections.Generic;
using System.Linq;
using AgentMulder.ReSharper.Domain.Patterns;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Psi;
#if SDK90
using JetBrains.ReSharper.Feature.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Feature.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Feature.Services.StructuralSearch;
#else
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
#endif
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Util;

namespace AgentMulder.Containers.Autofac.Patterns.FromAssemblies.BasedOn
{
    internal sealed class AsImplementedInterfaces : MultipleMatchBasedOnPatternBase
    {
        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$builder$.AsImplementedInterfaces()",
                new ExpressionPlaceholder("builder",
                    "global::Autofac.Builder.IRegistrationBuilder<object,global::Autofac.Features.Scanning.ScanningActivatorData,global::Autofac.Builder.DynamicRegistrationStyle>", false));

        public AsImplementedInterfaces()
            : base(pattern)
        {
        }

        protected override IEnumerable<FilteredRegistrationBase> DoCreateRegistrations(ITreeNode registrationRootElement, IStructuralMatchResult match)
        {
            yield return new ImplementedInterfacesRegistration(registrationRootElement);
        }

        private class ImplementedInterfacesRegistration : FilteredRegistrationBase
        {
            public ImplementedInterfacesRegistration(ITreeNode registrationRootElement)
                : base(registrationRootElement)
            {
                AddFilter(typeElement => typeElement.GetSuperTypes()
                                                    .SelectNotNull(type => type.GetTypeElement())
                                                    .OfType<IInterface>()
                                                    .Any(@interface => @interface.GetClrName().FullName != "System.IDisposable"));
            }
        }
    }
}
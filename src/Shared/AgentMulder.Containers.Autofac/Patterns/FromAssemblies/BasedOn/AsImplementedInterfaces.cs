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
    internal sealed class AsImplementedInterfaces : MultipleMatchBasedOnPatternBase
    {
        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$builder$.AsImplementedInterfaces()",
#if SDK70
                new ExpressionPlaceholder("builder", "global::Autofac.Builder.IRegistrationBuilder<,,>", false));
#else
                new ExpressionPlaceholder("builder",
                    "global::Autofac.Builder.IRegistrationBuilder<object,global::Autofac.Features.Scanning.ScanningActivatorData,global::Autofac.Builder.DynamicRegistrationStyle>", false));
#endif

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
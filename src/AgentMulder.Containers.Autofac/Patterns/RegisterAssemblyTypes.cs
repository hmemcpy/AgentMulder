using System;
using System.Collections.Generic;
using System.Linq;
using AgentMulder.Containers.Autofac.Patterns.Helpers;
using AgentMulder.Containers.Autofac.Registrations;
using AgentMulder.ReSharper.Domain.Elements.Modules;
using AgentMulder.ReSharper.Domain.Patterns;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.Autofac.Patterns
{
    public class RegisterAssemblyTypes : FromDescriptorPatternBase
    {
        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$builder$.RegisterAssemblyTypes($assemblies$)",
                new ExpressionPlaceholder("builder", "global::Autofac.ContainerBuilder", true),
                new ArgumentPlaceholder("assemblies", -1, -1));

        public RegisterAssemblyTypes(params IBasedOnPattern[] basedOnPatterns)
            : base(pattern, basedOnPatterns)
        {
            ModuleExtractor.AddExtractor(new ThisAssemblyExtractor());
        }

        public override IEnumerable<IComponentRegistration> GetComponentRegistrations(ITreeNode registrationRootElement)
        {
            IStructuralMatchResult match = Match(registrationRootElement);

            if (match.Matched)
            {
                var arguments = match.GetMatchedElementList("assemblies").Cast<ICSharpArgument>();

                IEnumerable<IModule> modules = arguments.Select(argument => ModuleExtractor.GetTargetModule(argument.Value)).ToList();

                foreach (IModule module in modules)
                {
                    yield return new ModuleBasedOnRegistration(module, new DefaultScanAssemblyRegistration(registrationRootElement));
                }
            }
        }
    }
}
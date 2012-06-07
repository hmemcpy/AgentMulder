using System;
using System.Collections.Generic;
using System.Linq;
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
        }

        public override IEnumerable<IComponentRegistration> GetComponentRegistrations(ITreeNode registrationRootElement)
        {
            IStructuralMatchResult match = Match(registrationRootElement);

            if (match.Matched)
            {
                var arguments = match.GetMatchedElementList("arguments").Cast<ICSharpArgument>();

                IEnumerable<IModule> modules = arguments.Select(ModuleExtractor.GetTargetModule).ToList();

                foreach (var basedOnPattern in BasedOnPatterns)
                {
                    var basedOnRegistrations = basedOnPattern.GetBasedOnRegistrations(registrationRootElement);

                    foreach (BasedOnRegistrationBase registration in basedOnRegistrations)
                    {
                        yield return new ModuleBasedOnRegistration(modules, registration);
                    }
                }
            }
        }
    }
}
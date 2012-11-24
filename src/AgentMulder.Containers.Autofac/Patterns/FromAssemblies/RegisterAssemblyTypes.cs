using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using AgentMulder.ReSharper.Domain.Elements.Modules;
using AgentMulder.ReSharper.Domain.Elements.Modules.Impl;
using AgentMulder.ReSharper.Domain.Patterns;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Util;

namespace AgentMulder.Containers.Autofac.Patterns.FromAssemblies
{
    [Export("ComponentRegistration", typeof(IRegistrationPattern))]
    public sealed class RegisterAssemblyTypes : RegistrationPatternBase
    {
        private readonly IEnumerable<IBasedOnPattern> basedOnPatterns;

        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$builder$.RegisterAssemblyTypes($assemblies$)",
                new ExpressionPlaceholder("builder", "global::Autofac.ContainerBuilder", true),
                new ArgumentPlaceholder("assemblies", -1, -1));

        [ImportingConstructor]
        public RegisterAssemblyTypes([ImportMany] IEnumerable<IBasedOnPattern> basedOnPatterns)
            : base(pattern)
        {
            this.basedOnPatterns = basedOnPatterns;
            ModuleExtractor.AddExtractor(new CallingAssemblyExtractor("Autofac.Module", "ThisAssembly"));
        }

        public override IEnumerable<IComponentRegistration> GetComponentRegistrations(ITreeNode registrationRootElement)
        {
            IExpressionStatement parentExpression = GetParentExpressionStatemenmt(registrationRootElement);
            if (parentExpression == null)
            {
                yield break;
            }

            IStructuralMatchResult match = Match(registrationRootElement);
            if (match.Matched)
            {
                var arguments = match.GetMatchedElementList("assemblies").Cast<ICSharpArgument>();

                IEnumerable<IModule> modules = arguments.SelectNotNull(argument => ModuleExtractor.GetTargetModule(argument.Value));

                IEnumerable<FilteredRegistrationBase> basedOnRegistrations = basedOnPatterns.SelectMany(
                    basedOnPattern => basedOnPattern.GetBasedOnRegistrations(parentExpression.Expression)).ToList();

                foreach (IModule module in modules)
                {
                    // todo blech, fix this
                    yield return new CompositeRegistration(registrationRootElement, basedOnRegistrations.Union(
                        new ComponentRegistrationBase[]
                        {
                            new DefaultScanAssemblyRegistration(registrationRootElement),
                            new ModuleBasedOnRegistration(registrationRootElement, module)
                        }));
                }
            }
        }

        private IExpressionStatement GetParentExpressionStatemenmt(ITreeNode node)
        {
            for (var n = node; n != null; n = n.Parent)
            {
                var expressionStatement = n as IExpressionStatement;
                if (expressionStatement != null)
                    return expressionStatement;
            }

            return null;
        }
    }
}
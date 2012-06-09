using System;
using System.Collections.Generic;
using System.Linq;
using AgentMulder.Containers.Autofac.Patterns.Helpers;
using AgentMulder.Containers.Autofac.Registrations;
using AgentMulder.ReSharper.Domain.Elements.Modules;
using AgentMulder.ReSharper.Domain.Patterns;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Util;

namespace AgentMulder.Containers.Autofac.Patterns.FromAssemblies
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

                foreach (IModule module in modules)
                {
                    var registration = new ModuleBasedOnRegistration(module, new DefaultScanAssemblyRegistration(registrationRootElement));

                    foreach (var basedOnRegistration in BasedOnPatterns.SelectMany(
                        basedOnPattern => basedOnPattern.GetBasedOnRegistrations(parentExpression.Expression)))
                    {
                        yield return new CompositeRegistration(registrationRootElement, registration, basedOnRegistration);
                    }
                }
            }
        }

        private class CompositeRegistration : ComponentRegistrationBase
        {
            private readonly IComponentRegistration[] registrations;

            public CompositeRegistration(ITreeNode registrationElement, params IComponentRegistration[] registrations)
                : base(registrationElement)
            {
                this.registrations = registrations;
            }

            public override bool IsSatisfiedBy(ITypeElement typeElement)
            {
                return registrations.All(registration => registration.IsSatisfiedBy(typeElement));
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
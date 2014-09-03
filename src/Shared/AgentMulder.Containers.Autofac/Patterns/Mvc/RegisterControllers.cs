using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using AgentMulder.Containers.Autofac.Registrations;
using AgentMulder.ReSharper.Domain.Elements.Modules;
using AgentMulder.ReSharper.Domain.Elements.Modules.Impl;
using AgentMulder.ReSharper.Domain.Patterns;
using AgentMulder.ReSharper.Domain.Registrations;
using AgentMulder.ReSharper.Domain.Utils;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Modules;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Util;

namespace AgentMulder.Containers.Autofac.Patterns.Mvc
{
    [Export("ComponentRegistration", typeof(IRegistrationPattern))]
    public sealed class RegisterControllers : RegistrationPatternBase
    {
        private readonly IEnumerable<IBasedOnPattern> basedOnPatterns;

        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$builder$.RegisterControllers($assemblies$)",
                new ExpressionPlaceholder("builder", "Autofac.ContainerBuilder", true),
                new ArgumentPlaceholder("assemblies"));

        [ImportingConstructor]
        public RegisterControllers([ImportMany] IEnumerable<IBasedOnPattern> basedOnPatterns)
            : base(pattern)
        {
            this.basedOnPatterns = basedOnPatterns;
        }

        public override IEnumerable<IComponentRegistration> GetComponentRegistrations(ITreeNode registrationRootElement)
        {
            IExpressionStatement parentExpression = GetParentExpressionStatement(registrationRootElement);
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
                    yield return new CompositeRegistration(registrationRootElement, basedOnRegistrations.Concat(
                        new ComponentRegistrationBase[]
                        {
                            new MvcControllerRegistration(registrationRootElement),
                            new ModuleBasedOnRegistration(registrationRootElement, module)
                        }));
                }
            }
        }

        public class MvcControllerRegistration : FilteredRegistrationBase
        {
            private const string MvcControllerClrTypeName = "System.Web.Mvc.IController";

            public MvcControllerRegistration(ITreeNode registrationRootElement)
                : base(registrationRootElement)
            {
                IDeclaredType mvcControllerType =
                    TypeFactory.CreateTypeByCLRName(MvcControllerClrTypeName,
                        registrationRootElement.GetPsiModule(),
                        registrationRootElement.GetResolveContext());

                AddFilter(element => element.GetSuperTypes().Any(type => type.IsSubtypeOf(mvcControllerType)));
            }
        }

        private IExpressionStatement GetParentExpressionStatement(ITreeNode node)
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

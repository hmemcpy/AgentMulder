using System;
using System.Collections.Generic;
using System.Linq;
using AgentMulder.ReSharper.Domain.Elements.Modules;
using AgentMulder.ReSharper.Domain.Patterns;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.Metadata.Reader.API;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Util;

namespace AgentMulder.Containers.Autofac.Patterns.Mvc
{
    public abstract class RegisterControllersBase : RegistrationPatternBase
    {
        private readonly string mvcTypeFqn;
        private readonly IEnumerable<IBasedOnPattern> basedOnPatterns;

        protected RegisterControllersBase(string expression, string mvcTypeFqn, IEnumerable<IBasedOnPattern> basedOnPatterns)
            : base(CreatePattern(expression))
        {
            this.mvcTypeFqn = mvcTypeFqn;
            this.basedOnPatterns = basedOnPatterns;
        }

        private static IStructuralSearchPattern CreatePattern(string expression)
        {
            return new CSharpStructuralSearchPattern(string.Format("$builder$.{0}($assemblies$)", expression),
                new ExpressionPlaceholder("builder", "Autofac.ContainerBuilder", true),
                new ArgumentPlaceholder("assemblies"));
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

                IEnumerable<IModule> modules = arguments.SelectNotNull(argument => ModuleExtractor.GetTargetModule<ICSharpExpression>(argument.Value));

                IEnumerable<FilteredRegistrationBase> basedOnRegistrations = basedOnPatterns.SelectMany(
                    basedOnPattern => basedOnPattern.GetBasedOnRegistrations(parentExpression.Expression)).ToList();

                foreach (IModule module in modules)
                {
                    // todo blech, fix this
                    yield return new CompositeRegistration(registrationRootElement, basedOnRegistrations.Concat(
                        new ComponentRegistrationBase[]
                        {
                            new MvcControllerRegistration(registrationRootElement, mvcTypeFqn),
                            new ModuleBasedOnRegistration(registrationRootElement, module)
                        }));
                }
            }
        }

        private class MvcControllerRegistration : FilteredRegistrationBase
        {
            public MvcControllerRegistration(ITreeNode registrationRootElement, string mvcControllerClrTypeName)
                : base(registrationRootElement)
            {
                AddFilter(typeElement =>
                {
                    ITypeElement mvcControllerType = TypeFactory.CreateTypeByCLRName(
                        mvcControllerClrTypeName, 
                        typeElement.Module,
                        typeElement.ResolveContext).GetTypeElement();

                    return typeElement.IsDescendantOf(mvcControllerType);
                });
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

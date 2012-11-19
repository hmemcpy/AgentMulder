using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using AgentMulder.ReSharper.Domain.Elements.Modules;
using AgentMulder.ReSharper.Domain.Elements.Modules.Impl;
using AgentMulder.ReSharper.Domain.Patterns;
using AgentMulder.ReSharper.Domain.Registrations;
using AgentMulder.ReSharper.Domain.Utils;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Util;

namespace AgentMulder.Containers.StructureMap.Patterns.Scan
{
    [InheritedExport("ComponentRegistration", typeof(IRegistrationPattern))]
    internal abstract class ScanPatternBase : FromDescriptorPatternBase
    {
        private readonly IEnumerable<FromAssemblyPatternBase> fromAssemblyPatterns;

        protected ScanPatternBase(IStructuralSearchPattern pattern, IEnumerable<FromAssemblyPatternBase> fromAssemblyPatterns, IEnumerable<IBasedOnPattern> basedOnPatterns)
            : base(pattern, basedOnPatterns)
        {
            this.fromAssemblyPatterns = fromAssemblyPatterns;
        }

        public override IEnumerable<IComponentRegistration> GetComponentRegistrations(ITreeNode registrationRootElement)
        {
            // todo this is duplicated, needs refactoring!
            // This entire thing is one big hack. Need to come back to it one day :)
            // There is (currently) no way to create a pattern that would match the Scan() call with implicit 'this' in ReSharper SSR
            // (i.e. a call to Scan being made withing a method, located in the Registry class)
            // Therefore I'm only matching by the method name only, and later verifying that the method indeed belongs to StructureMap, by
            // making sure the invocation's qualifier derived from global::StructureMap.Configuration.DSL.IRegistry

            if (!registrationRootElement.IsContainerCall(Constants.StructureMapRegistryTypeName))
            {
                yield break;
            }

            IInvocationExpression invocationExpression = registrationRootElement.GetInvocationExpression();
            if (invocationExpression == null)
            {
                yield break;
            }

            IStructuralMatchResult match = Match(invocationExpression);

            if (match.Matched)
            {
                var invocationExpressions = match.GetMatchedElementList("statements")
                                                 .Cast<IExpressionStatement>()
                                                 .Select(statement => statement.Expression)
                                                 .OfType<IInvocationExpression>()
                                                 .ToList();

                var registrations = (from expression in invocationExpressions
                                     from basedOnPattern in BasedOnPatterns
                                     from registration in basedOnPattern.GetBasedOnRegistrations(expression)
                                     select registration).ToList();

                if (registrations.Any())
                {
                    var moduleRegistrations = (from expression in invocationExpressions
                                               from pattern in fromAssemblyPatterns
                                               from registration in pattern.GetComponentRegistrations(expression)
                                               select registration).ToList();

                    yield return new CompositeRegistration(registrationRootElement, registrations.Union(moduleRegistrations));
                }
            }
        }
    }
}
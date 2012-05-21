using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Xml.Linq;
using AgentMulder.ReSharper.Domain.Utils;
using ExpressionSerialization;
using JetBrains.ProjectModel;
using JetBrains.ProjectModel.Model2.Assemblies.Interfaces;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Util;

namespace AgentMulder.ReSharper.Domain.Registrations
{
    public class TypePredicateRegistration : BasedOnRegistrationBase
    {
        private readonly IModule targetModule;
        private readonly Expression predicateExpression;

        public TypePredicateRegistration(ITreeNode registrationRootElement, Expression<Predicate<Type>> predicateExpression, IEnumerable<WithServiceRegistration> withServices)
            : base(registrationRootElement, withServices)
        {
            this.targetModule = targetModule;
            this.predicateExpression = predicateExpression;
        }

        public override bool IsSatisfiedBy(ITypeElement typeElement)
        {
            // todo fix me!
            // find a way to add the module
            IAssembly moduleAssembly = targetModule.GetModuleAssembly();
            if (moduleAssembly == null)
            {
                return false;
            }
            var psiAssembly = moduleAssembly as IPsiAssembly;
            if (psiAssembly == null)
            {
                return false;
            }
            var assemblyFilePath = psiAssembly.Location;
            if (assemblyFilePath == null)
            {
                return false;
            }

            IEnumerable<string> matchedTypesNames;
            if (!TryLoadAssembly(assemblyFilePath, out matchedTypesNames))
            {
                return false;
            }
            
            return matchedTypesNames.Contains(typeElement.GetClrName().FullName);
        }

        private static readonly Dictionary<FileSystemPath, Assembly> cache = new Dictionary<FileSystemPath, Assembly>(); 

        private bool TryLoadAssembly(FileSystemPath assemblyFilePath, out IEnumerable<string> matchedTypesNames)
        {
            var loader = new SeperateAppDomainAssemblyLoader();

            var serializer = new ExpressionSerializer();
            XElement xElement = serializer.Serialize(predicateExpression);

            matchedTypesNames = loader.GetTypesFromAssembly(assemblyFilePath.ToFileInfo(), xElement.ToString());

            return true;
        }

        
    }
}
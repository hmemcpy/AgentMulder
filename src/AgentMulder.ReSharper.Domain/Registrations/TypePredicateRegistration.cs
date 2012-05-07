using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Xml.Linq;
using AgentMulder.ReSharper.Domain.Utils;
using ExpressionSerialization;
using JetBrains.DocumentModel;
using JetBrains.ProjectModel;
using JetBrains.ProjectModel.Model2.Assemblies.Interfaces;
using JetBrains.ReSharper.Psi;
using JetBrains.Util;

namespace AgentMulder.ReSharper.Domain.Registrations
{
    public class TypePredicateRegistration : BasedOnRegistrationBase
    {
        private readonly Expression predicateExpression;

        public TypePredicateRegistration(DocumentRange documentRange, Expression<Predicate<Type>> predicateExpression, IEnumerable<WithServiceRegistration> withServices)
            : base(documentRange, withServices)
        {
            this.predicateExpression = predicateExpression;
        }

        public override bool IsSatisfiedBy(ITypeElement typeElement)
        {
            FileSystemPath assemblyFilePath = GetModuleAssembly();
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

        private FileSystemPath GetModuleAssembly()
        {
            var assemblyPsiModule = Module as IAssemblyPsiModule;
            if (assemblyPsiModule != null)
            {
                return assemblyPsiModule.Assembly.Location;
                
            }
            var project = Module as IProject;
            if (project != null)
            {
                IAssemblyFile outputAssemblyFile = project.GetOutputAssemblyFile();
                var data = outputAssemblyFile as IAssemblyFileData;
                if (data != null)
                {
                    return data.Location;
                }

                return null;
            }

            return null;
        }
    }
}
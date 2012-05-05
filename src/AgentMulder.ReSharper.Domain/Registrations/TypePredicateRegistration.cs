using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JetBrains.DocumentModel;
using JetBrains.ProjectModel;
using JetBrains.ProjectModel.Model2.Assemblies.Interfaces;
using JetBrains.ReSharper.Psi;
using JetBrains.Util;

namespace AgentMulder.ReSharper.Domain.Registrations
{
    public class TypePredicateRegistration : BasedOnRegistrationBase
    {
        private readonly Predicate<Type> predicate;

        public TypePredicateRegistration(DocumentRange documentRange, Predicate<Type> predicate, IEnumerable<WithServiceRegistration> withServices)
            : base(documentRange, withServices)
        {
            this.predicate = predicate;
        }

        public override bool IsSatisfiedBy(ITypeElement typeElement)
        {
            FileSystemPath assemblyFilePath = GetModuleAssembly();
            if (assemblyFilePath == null)
            {
                return false;
            }

            var assembly = TryLoadAsembly(assemblyFilePath);
            if (assembly == null)
            {
                return false;
            }

            IEnumerable<string> matchedTypesNames = assembly.GetTypes().Where(t => predicate(t)).Select(type => type.FullName);

            // todo 1) convert module to assembly
            //      2) load assembly, get list of type names matching predicate
            //      3) compare typeElement full name to list

            return matchedTypesNames.Contains(typeElement.GetClrName().FullName);
        }

        private Assembly TryLoadAsembly(FileSystemPath assemblyFilePath)
        {
            try
            {
                return Assembly.ReflectionOnlyLoadFrom(assemblyFilePath.FullPath);
            }
            catch
            {
                return null;
            }
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
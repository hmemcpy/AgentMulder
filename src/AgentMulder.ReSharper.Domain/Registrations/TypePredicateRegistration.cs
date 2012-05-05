using System;
using System.Collections.Generic;
using JetBrains.DocumentModel;
using JetBrains.Metadata.Reader.API;
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
            using (MetadataLoader l = new MetadataLoader())
            {
                var assemblyPsiModule = typeElement.Module as IAssemblyPsiModule;
                if (assemblyPsiModule != null)
                {
                    FileSystemPath fileSystemPath = assemblyPsiModule.Assembly.Location;
                    var metadataAssembly = l.LoadFrom(fileSystemPath, info => true);
                }
                var project = typeElement.Module.ContainingProjectModule as IProject;
                if (project != null)
                {
                    var outputAssemblyFile = project.GetOutputAssemblyFile();
                    var outputAssemblyName = project.GetOutputAssemblyName();
                    var fileSystemPath = project.ActiveConfiguration.OutputDirectory;
                }
            }


            // todo 1) convert module to assembly
            //      2) load assembly, get list of type names matching predicate
            //      3) compare typeElement full name to list
            
            return false;
        }
    }
}
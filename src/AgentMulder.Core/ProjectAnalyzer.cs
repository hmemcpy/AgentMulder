using System;
using System.Collections.Generic;
using System.Linq;
using AgentMulder.Core.Patterns;
using AgentMulder.Core.ProjectModel;

namespace AgentMulder.Core
{
    public class ProjectAnalyzer
    {
        private readonly IRegistrationParser registrationParser;

        public ProjectAnalyzer(IRegistrationParser registrationParser)
        {
            this.registrationParser = registrationParser;
        }

        public IEnumerable<IComponentRegistration> GetRegisteredTypes(IProject project)
        {
            return project.Files.SelectMany(file => registrationParser.Parse(project.Compilation, file.CompilationUnit));
        }
    }
}
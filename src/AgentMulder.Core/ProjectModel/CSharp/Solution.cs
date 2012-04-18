using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using ICSharpCode.NRefactory.TypeSystem;
using ICSharpCode.NRefactory.TypeSystem.Implementation;

namespace AgentMulder.Core.ProjectModel.CSharp
{
    internal class Solution : ISolution
    {
        private readonly List<IProject> projects = new List<IProject>();
        private readonly ISolutionSnapshot solutionSnapshot = new DefaultSolutionSnapshot();

        public ISolutionSnapshot SolutionSnapshot
        {
            get { return solutionSnapshot; }
        }

        public IEnumerable<IProject> Projects
        {
            get { return projects; }
        }

        public Solution(string fileName)
        {
            if (fileName == null) throw new ArgumentNullException("fileName");
            if (!File.Exists(fileName)) throw new InvalidOperationException(string.Format("Solution file '{0}' does not exist", fileName));

            var directory = Path.GetDirectoryName(fileName);
            var projectLinePattern = new Regex("Project\\(\"(?<TypeGuid>.*)\"\\)\\s+=\\s+\"(?<Title>.*)\",\\s*\"(?<Location>.*)\",\\s*\"(?<Guid>.*)\"");
            foreach (string line in File.ReadLines(fileName))
            {
                Match match = projectLinePattern.Match(line);
                if (match.Success)
                {
                    string typeGuid = match.Groups["TypeGuid"].Value;
                    string title = match.Groups["Title"].Value;
                    string location = match.Groups["Location"].Value;
                    string guid = match.Groups["Guid"].Value;
                    switch (typeGuid.ToUpperInvariant())
                    {
                        case "{2150E333-8FDC-42A3-9474-1A3956D46DE8}": // Solution Folder
                            // ignore folders
                            break;
                        case "{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}": // C# project
                            projects.Add(new CSharpProject(this, title, Path.Combine(directory, location)));
                            break;
                        default:
                            // todo: unsupported project type (i.e VB at the moment)
                            break;
                    }
                }
            }

        }
    }
}
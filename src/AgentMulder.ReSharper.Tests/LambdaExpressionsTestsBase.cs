using System;
using System.Linq;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.TestFramework;
using NUnit.Framework;

namespace AgentMulder.ReSharper.Tests
{
    [Category("Expression Trees")]
    public abstract class LambdaExpressionsTestsBase : BaseTestWithSingleProject
    {
        protected override string RelativeTestDataPath
        {
            get { return @"Expressions"; }
        }

        protected virtual string RelativeTypesPath
        {
            get { return "..\\Types"; }
        }

        protected void RunTest(string testName, Action<ILambdaExpression> action)
        {
            string fileName = testName + Extension;

            WithSingleProject(fileName, (lifetime, project) => RunGuarded(() =>
            {
                ICSharpFile file = GetCodeFile(fileName);

                file.ProcessChildren(action);
            }));
        }

        private ICSharpFile GetCodeFile(string fileName)
        {
            PsiManager manager = PsiManager.GetInstance(Solution);
            IProjectFile projectFile = Project.GetAllProjectFiles(file => file.Name.Equals(fileName, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            if (projectFile != null)
            {
                return manager.GetPsiFile(projectFile.ToSourceFile(), CSharpLanguage.Instance) as ICSharpFile;
            }

            return null;
        }
    }
}
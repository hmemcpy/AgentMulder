using System;
using System.Collections.Generic;
using JetBrains.ProjectModel;
using JetBrains.ProjectModel.Model2.Assemblies.Interfaces;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Util;

namespace AgentMulder.ReSharper.Domain.Utils
{
    public static class PsiExtensions
    {
        public static IEnumerable<IInvocationExpression> GetAllExpressions(this IInvocationExpression expression)
        {
            for (var e = expression; e != null; e = ((IReferenceExpression)e.InvokedExpression).QualifierExpression as IInvocationExpression)
                yield return e;
        }

        public static bool IsGenericInterface(this ITypeElement typeElement)
        {
            return typeElement is IInterface &&
                   typeElement.HasTypeParameters();
		}
		
        public static FileSystemPath GetModuleAssemblyLocation(this IModule module)
        {
            var assemblyPsiModule = module as IAssemblyPsiModule;
            if (assemblyPsiModule != null)
            {
                return assemblyPsiModule.Assembly.Location;

            }
            var project = module as IProject;
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

        public static IAssembly GetModuleAssembly(this IModule module)
        {
            var assemblyPsiModule = module as IAssemblyPsiModule;
            if (assemblyPsiModule != null)
            {
                return assemblyPsiModule.Assembly.ToAssembly();

            }
            var project = module as IProject;
            if (project != null)
            {
                IAssemblyFile outputAssemblyFile = project.GetOutputAssemblyFile();
                if (outputAssemblyFile != null)
                {
                    return outputAssemblyFile.Assembly;
                }
                return null;
            }

            return null;
        }

        public static bool IsGenericTypeDefinition(this ITypeElement element)
        {
            // todo check if this is enoguh
            return element.TypeParameters.Count > 0;
        }

        public static bool IsDelegate(this ITypeElement element)
        {
            // todo check if true;
            return element is IDelegate;
        }

        public static INamespace GetNamespaceDeclaration(ICSharpExpression expression)
        {
            CSharpElementFactory elementFactory = CSharpElementFactory.GetInstance(expression.GetPsiModule());

            if (expression.ConstantValue != null &&
                expression.ConstantValue.IsString())
            {
                string namespaceName = Convert.ToString(expression.ConstantValue.Value);

                return elementFactory.CreateNamespaceDeclaration(namespaceName).DeclaredElement;
            }

            return null;
        }

        public static TExpression GetParentExpression<TExpression>(this ITreeNode node)
            where TExpression : class, ITreeNode
        {
            for (var n = node; n != null; n = n.Parent)
            {
                var expressionStatement = n as TExpression;
                if (expressionStatement != null)
                    return expressionStatement;
            }

            return null;
        }

        public static IInvocationExpression GetInvocationExpression(this ITreeNode node)
        {
            // first, try to find the parent statement expression
            var statement = node.GetParentExpression<IExpressionStatement>();
            if (statement != null)
            {
                return statement.Expression as IInvocationExpression;
            }

            // otherwise, try finding the lambda expression, and take its invocation
            var lambdaExpression = node.GetParentExpression<ILambdaExpression>();
            if (lambdaExpression != null)
            {
                return lambdaExpression.BodyExpression as IInvocationExpression;
            }

            return null;
        }
    }
}
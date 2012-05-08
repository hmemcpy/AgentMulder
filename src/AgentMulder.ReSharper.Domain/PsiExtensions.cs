﻿using System;
using System.Collections.Generic;
﻿using System.Reflection;
﻿using JetBrains.ProjectModel;
﻿using JetBrains.ProjectModel.Model2.Assemblies.Interfaces;
﻿using JetBrains.ReSharper.Psi;
﻿using JetBrains.ReSharper.Psi.CSharp.Tree;
﻿using JetBrains.Util;

namespace AgentMulder.ReSharper.Domain
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
    }
}
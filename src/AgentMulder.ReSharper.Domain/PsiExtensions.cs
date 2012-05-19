﻿using System;
using System.Collections.Generic;
﻿using JetBrains.ReSharper.Psi;
﻿using JetBrains.ReSharper.Psi.CSharp.Tree;

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
    }
}
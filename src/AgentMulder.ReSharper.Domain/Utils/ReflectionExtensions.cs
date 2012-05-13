using System;
using JetBrains.ReSharper.Psi;

namespace AgentMulder.ReSharper.Domain.Utils
{
    public static class ReflectionExtensions
    {
        public static Type ToReflectionType(this IType psiType)
        {
            return Type.GetType(psiType.GetScalarType().GetClrName().FullName);
        }
    }
}
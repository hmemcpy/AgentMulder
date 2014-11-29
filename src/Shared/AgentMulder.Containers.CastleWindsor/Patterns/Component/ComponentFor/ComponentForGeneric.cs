using System.Collections.Generic;
using System.Text;
using AgentMulder.ReSharper.Domain.Patterns;
#if SDK90
using JetBrains.ReSharper.Feature.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Feature.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Feature.Services.StructuralSearch;
#else
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
#endif

namespace AgentMulder.Containers.CastleWindsor.Patterns.Component.ComponentFor
{
    internal sealed class ComponentForGeneric : ComponentForBasePattern
    {
        public ComponentForGeneric(int forwardersCount, params ComponentImplementationPatternBase[] implementedByPatterns)
            : base(CreatePattern(forwardersCount), "service", implementedByPatterns)
        {
        }

        public ComponentForGeneric(params ComponentImplementationPatternBase[] implementedByPatterns)
            : base(CreatePattern(0), "service", implementedByPatterns)
        {
        }

        private static IStructuralSearchPattern CreatePattern(int forwardersCount)
        {
            string patternFragment;
            TypePlaceholder[] forwarderPlaceholders = CreateForwarderPlaceholders(forwardersCount, out patternFragment);

            string patternText = forwarderPlaceholders.Length == 0
                                     ? "$component$.For<$service$>()"
                                     : string.Format("$component$.For<$service$, {0}>()", patternFragment);

            var pattern = new CSharpStructuralSearchPattern(patternText,
                            new ExpressionPlaceholder("component", "Castle.MicroKernel.Registration.Component"),
                            new TypePlaceholder("service"));

            foreach (TypePlaceholder placeholder in forwarderPlaceholders)
            {
                pattern.Placeholders[placeholder.Name] = placeholder;
            }

            return pattern;
        }

        private static TypePlaceholder[] CreateForwarderPlaceholders(int count, out string patternFragment)
        {
            var placeholders = new List<TypePlaceholder>(count);
            var sb = new StringBuilder();

            for (int i = 1; i <= count; i++)
            {
                var placeholder = new TypePlaceholder("forward" + i);
                sb.AppendFormat("${0}$", placeholder.Name);
                if (i < count)
                {
                    sb.Append(',');
                }
                placeholders.Add(placeholder);
            }

            patternFragment = sb.ToString();
            return placeholders.ToArray();
        }
    }
}
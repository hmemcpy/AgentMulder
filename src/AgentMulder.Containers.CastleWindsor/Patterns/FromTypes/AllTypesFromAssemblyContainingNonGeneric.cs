using AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.BasedOn;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.Containers.CastleWindsor.Patterns.FromTypes
{
    internal sealed class AllTypesFromAssemblyContainingNonGeneric : ClassesFromAssemblyBasePattern
    {
        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$alltypes$.FromAssemblyContaining($argument$)",
                                              new ExpressionPlaceholder("alltypes", "Castle.MicroKernel.Registration.AllTypes"),
                                              new ArgumentPlaceholder("argument", 1, 1));

        public AllTypesFromAssemblyContainingNonGeneric(params BasedOnRegistrationBasePattern[] basedOnPatterns)
            : base(pattern, basedOnPatterns)
        {
        }

        protected override IModule GetTargetModule(IStructuralMatchResult match)
        {
            var argument = match.GetMatchedElement("argument") as ICSharpArgument;
            if (argument == null)
            {
                return null;
            }
            
            var typeofExpression = argument.Value as ITypeofExpression;
            if (typeofExpression != null)
            {
                var declaredType = typeofExpression.ArgumentType as IDeclaredType;
                if (declaredType == null)
                {
                    return null;
                }

                var typeElement = declaredType.GetTypeElement();
                if (typeElement != null)
                {
                    return typeElement.Module.ContainingProjectModule;
                }
            }

            return null;
        }
    }
}
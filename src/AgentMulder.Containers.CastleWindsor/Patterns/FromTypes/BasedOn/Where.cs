using System;
using System.Collections.Generic;
using System.Linq;
using AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.WithService;
using AgentMulder.ReSharper.Domain;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.BasedOn
{
    internal sealed class Where : BasedOnRegistrationBasePattern
    {
        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$fromDescriptor$.Where($predicate$)",
                new ExpressionPlaceholder("fromDescriptor", "Castle.MicroKernel.Registration.FromDescriptor", false),
                new ExpressionPlaceholder("predicate"));
        
        public Where(params WithServiceRegistrationBasePattern[] withServicePatterns)
            : base(pattern, withServicePatterns)
        {
        }

        public override IEnumerable<IComponentRegistration> GetComponentRegistrations(ITreeNode parentElement)
        {
            IStructuralMatchResult match = Match(parentElement);

            if (match.Matched)
            {

                var lambdaExpression = match.GetMatchedElement("predicate") as ILambdaExpression;
                if (lambdaExpression != null)
                {
                    // get all type elements in a target module
                    // build a predicate
                    // match target element with predicate
                    
                    IEnumerable<ITypeElement> matchedTypes = GetMatchedTypes(lambdaExpression).ToList();

                    foreach (var basedOnRegistration in base.GetComponentRegistrations(parentElement).OfType<BasedOnRegistration>())
                    {
                        yield return new TypesBasedOnRegistration(matchedTypes, basedOnRegistration);
                    }
                }
            }
        }

        private IEnumerable<ITypeElement> GetMatchedTypes(ILambdaExpression lambdaExpression)
        {
            Predicate<ITypeElement> predicate = WherePredicateBuilder.FromLambda<ITypeElement>(lambdaExpression);

            return GetTypeDeclarations(Module).Select(declaration => declaration.DeclaredElement).ToList();
        }

        private IEnumerable<ITypeDeclaration> GetTypeDeclarations(IPsiModule module)
        {
            ISolution solution = module.GetSolution();
            PsiManager manager = PsiManager.GetInstance(solution);

            var declarations = new List<ITypeDeclaration>();

            foreach (IPsiSourceFile sourceFile in module.SourceFiles)
            {
                var file  = ((ICSharpFile)manager.GetPsiFile(sourceFile, sourceFile.PrimaryPsiLanguage));

                file.ProcessChildren<ITypeDeclaration>(declarations.Add);
            }

            return declarations;
        }
    }
}
using System;
using AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.BasedOn;
using AgentMulder.ReSharper.Domain.Modules;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.Containers.CastleWindsor.Patterns.FromTypes
{
    internal class FromAssembly : FromAssemblyBasePattern
    {
        private readonly Predicate<ITypeElement> filter;

        protected override Predicate<ITypeElement> Filter
        {
            get { return filter; }
        }

        public FromAssembly(string qualiferType, params BasedOnRegistrationBasePattern[] basedOnPatterns)
            : this(qualiferType, element => true, basedOnPatterns)
        {
        }

        public FromAssembly(string qualiferType, Predicate<ITypeElement> filter, params BasedOnRegistrationBasePattern[] basedOnPatterns)
            : base(CreatePattern(qualiferType), basedOnPatterns)
        {
            this.filter = filter;
        }

        private static IStructuralSearchPattern CreatePattern(string qualiferType)
        {
            return new CSharpStructuralSearchPattern("$qualifier$.FromAssembly($assembly$)",
                new ExpressionPlaceholder("qualifier", qualiferType, true),
                new ArgumentPlaceholder("assembly", 1, 1)); // matches exactly one argument
        }

        protected override IModule GetTargetModule(IStructuralMatchResult match)
        {
            var argument = (ICSharpArgument)match.GetMatchedElement("assembly");

            return ModuleExtractor.Instance.GetTargetModule(argument.Value);
        }
    }





    internal abstract class ClassesFromAssemblyBasePattern : FromAssemblyBasePattern
    {
        protected ClassesFromAssemblyBasePattern(IStructuralSearchPattern pattern, params BasedOnRegistrationBasePattern[] basedOnPatterns)
            : base(pattern, basedOnPatterns)
        {
        }

        protected override Predicate<ITypeElement> Filter
        {
            get
            {
                return typeElement =>
                {
                    var @class = typeElement as IClass;
                    if (@class != null)
                    {
                        return !@class.IsAbstract;
                    }

                    return false;
                };
            }
        }
    }
}
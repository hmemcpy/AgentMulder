using System.Collections.Generic;
using AgentMulder.Containers.CastleWindsor.Patterns.FromTypes.BasedOn;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.CastleWindsor.Patterns.FromTypes
{
    // todo not implemented yet
    //internal sealed class ClassesFrom : FromTypesBasePattern
    //{
    //    private static readonly IStructuralSearchPattern pattern =
    //        new CSharpStructuralSearchPattern("$classes$.From($services$)",
    //            new ExpressionPlaceholder("classes", "Castle.MicroKernel.Registration.Classes"),
    //            new ArgumentPlaceholder("services", -1, -1)); // matches any number of arguments

    //    public ClassesFrom(params BasedOnRegistrationBasePattern[] basedOnPatterns)
    //        : base(pattern, basedOnPatterns)
    //    {
    //    }

    //    public override IEnumerable<IComponentRegistration> GetComponentRegistrations(ITreeNode parentElement)
    //    {
    //        yield break;
    //    }
    //}
}
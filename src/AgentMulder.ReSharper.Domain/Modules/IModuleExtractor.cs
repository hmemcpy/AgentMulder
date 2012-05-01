using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace AgentMulder.ReSharper.Domain.Modules
{
    public interface IModuleExtractor
    {
        IModule GetTargetModule(ICSharpExpression expression);
    }
}
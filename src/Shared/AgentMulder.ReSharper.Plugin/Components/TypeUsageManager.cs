using System;
using JetBrains.ReSharper.Daemon.UsageChecking;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.ReSharper.Plugin.Components
{
    internal class TypeUsageManager : ITypeUsageManager
    {
        private readonly CollectUsagesStageProcess collectUsagesStageProcess;

        public TypeUsageManager(CollectUsagesStageProcess collectUsagesStageProcess)
        {
            if (collectUsagesStageProcess == null)
            {
                throw new InvalidOperationException("collectUsagesStageProcess is null");
            }

            this.collectUsagesStageProcess = collectUsagesStageProcess;
        }

        public void MarkTypeAsUsed(ITypeDeclaration declaration)
        {
            SetConstructorsState(declaration.DeclaredElement, UsageState.USED_MASK | UsageState.CANNOT_BE_PRIVATE |
                                                         UsageState.CANNOT_BE_INTERNAL | UsageState.CANNOT_BE_PROTECTED);

            collectUsagesStageProcess.SetElementState(declaration.DeclaredElement, UsageState.ACCESSED);
        }

        private void SetConstructorsState(ITypeElement typeElement, UsageState state)
        {
            foreach (IConstructor constructor in typeElement.Constructors)
            {
                collectUsagesStageProcess.SetElementState(constructor, state);
            }
        }
    }
}
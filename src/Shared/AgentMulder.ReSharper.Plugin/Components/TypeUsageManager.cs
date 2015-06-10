using System;
using JetBrains.Annotations;
using JetBrains.ReSharper.Daemon.UsageChecking;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Files;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Util;

namespace AgentMulder.ReSharper.Plugin.Components
{
    internal class TypeUsageManager
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
            //ITypeElement element = declaration.DeclaredElement;

            //UsageState mask = UsageState.USED_MASK |
            //                  UsageState.CANNOT_BE_PRIVATE |
            //                  UsageState.CANNOT_BE_INTERNAL |
            //                  UsageState.CANNOT_BE_PROTECTED;

            //SetConstructorsState(element, mask);

            //collectUsagesStageProcess.SetElementState(element, UsageState.ACCESSED);
        }

        private void SetConstructorsState(ITypeElement typeElement, UsageState state)
        {
            foreach (IConstructor constructor in typeElement.Constructors)
            {
                collectUsagesStageProcess.SetElementState(constructor, state);
            }
        }

        [NotNull]
        private static readonly Key ImplicitlyUsedCtorsKey = new Key("Implicitly used constructors");
    }
}
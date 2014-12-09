﻿using System.Collections.Generic;
using System.Reflection;
﻿using AgentMulder.ReSharper.Plugin.Daemon;
﻿using JetBrains.Annotations;
using JetBrains.Application;
﻿using JetBrains.Application.BuildScript.Solution;
﻿using JetBrains.TestFramework;
﻿using JetBrains.Threading;
using NUnit.Framework;
 
using JetBrains.Application.BuildScript.Application.Zones;
﻿using JetBrains.ReSharper.Resources.Shell;
using JetBrains.ReSharper.TestFramework;
using JetBrains.TestFramework.Application.Zones;

[assembly: TestDataPathBase(@"..\Test\Data")]

[ZoneDefinition]
public class TestEnvironmentZone : ITestsZone, IRequire<PsiFeatureTestZone>
{ 
}

[SetUpFixture]
public class ReSharperTestEnvironmentAssembly : TestEnvironmentAssembly<TestEnvironmentZone>
{
    [NotNull]
    private static IEnumerable<Assembly> GetAssembliesToLoad()
    {
        yield return Assembly.GetExecutingAssembly();
    }

    public override void SetUp()
    {
        base.SetUp();
        ReentrancyGuard.Current.Execute("LoadAssemblies", () =>
        {
            var assemblyManager = Shell.Instance.GetComponent<AssemblyManager>();
            assemblyManager.LoadAssemblies(GetType().Name, GetAssembliesToLoad());
        });
    }

    public override void TearDown()
    {
        ReentrancyGuard.Current.Execute("UnloadAssemblies", () =>
        {
            var assemblyManager = Shell.Instance.GetComponent<AssemblyManager>();
            assemblyManager.UnloadAssemblies(GetType().Name, GetAssembliesToLoad());
        });

        base.TearDown();
    }
}
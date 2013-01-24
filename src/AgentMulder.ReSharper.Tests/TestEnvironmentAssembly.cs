using AgentMulder.ReSharper.Plugin.Components;
using JetBrains.Application;
using JetBrains.ReSharper;
using JetBrains.Threading;
using System.Reflection;
using System.Collections.Generic;
using NUnit.Framework;

/// <summary>
/// Test environment. Must be in the global namespace.
/// </summary>
// ReSharper disable CheckNamespace

// Note: per suggestion by Matt Ellis, create an isolated environment, to prevent ReSharper from
// loading 3rd party plugins into the test environment. For more information: http://youtrack.jetbrains.com/issue/RSRP-337526
public class IsolatedReSharperTestEnvironmentAssembly : ReSharperTestEnvironmentAssembly
{
    public override IApplicationDescriptor CreateApplicationDescriptor()
    {
        return new IsolatedReSharperApplicationDescriptor();
    }

    private class IsolatedReSharperApplicationDescriptor : ReSharperApplicationDescriptor
    {
        public override string ProductName
        {
            get
            {
                // The product name is used to find settings under APPDATA. Leaving it at
                // ReSharper would use your normal ReSharper settings (+plugins!) while
                // running tests. This keeps them isolated
                return base.ProductName + "_Isolated";
            }
        }
    }
}

[SetUpFixture]
public class TestEnvironmentAssembly : IsolatedReSharperTestEnvironmentAssembly
{
  /// <summary>
  /// Gets the assemblies to load into test environment.
  /// Should include all assemblies which contain components.
  /// </summary>
  private static IEnumerable<Assembly> GetAssembliesToLoad()
  {
    // Test assembly
    yield return Assembly.GetExecutingAssembly();

    yield return typeof(SolutionAnalyzer).Assembly;
  }

  public override void SetUp()
  {
    base.SetUp();
    ReentrancyGuard.Current.Execute(
      "LoadAssemblies",
      () => Shell.Instance.GetComponent<AssemblyManager>().LoadAssemblies(
        GetType().Name, GetAssembliesToLoad()));
  }

  public override void TearDown()
  {
    ReentrancyGuard.Current.Execute(
      "UnloadAssemblies",
      () => Shell.Instance.GetComponent<AssemblyManager>().UnloadAssemblies(
        GetType().Name, GetAssembliesToLoad()));
    base.TearDown();
  }
}

// ReSharper restore CheckNamespace
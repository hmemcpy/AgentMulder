using System;
using AgentMulder.ReSharper.Plugin.Components;
using JetBrains.Application;
using JetBrains.ReSharper;
using JetBrains.Threading;
using System.Reflection;
using System.Collections.Generic;
using AgentMulder.ReSharper.Tests;
using JetBrains.Build.AllAssemblies;
using NUnit.Framework;

/// <summary>
/// Test environment. Must be in the global namespace.
/// </summary>
// ReSharper disable CheckNamespace

// Note: per suggestion by Matt Ellis, create an isolated environment, to prevent ReSharper from
// loading 3rd party plugins into the test environment. For more information: http://youtrack.jetbrains.com/issue/RSRP-337526
public abstract class IsolatedReSharperTestEnvironmentAssembly : ReSharperTestEnvironmentAssembly
{
    public override IApplicationDescriptor CreateApplicationDescriptor()
    {
        return new CustomReSharperApplicationDescriptor();
    }

    private class CustomReSharperApplicationDescriptor : ReSharperApplicationDescriptor
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

        // ReSharper 8.2 doesn't know anything about .net 4.6, and throws exceptions.
        // We can teach it by overriding the default FrameworkLocationHelper class, but
        // it must be overridden before the ShellComponents are composed, and the call
        // to AssemblyManager in SetUp is too late for that. So, we add this assembly
        // into the list of product assemblies that are used by default when composing
        // the Shell container. Hacky, but at least it lets us run the tests.
        private AllAssembliesXml allAssembliesXml;

        public override AllAssembliesXml AllAssembliesXml
        {
            get
            {
                if (allAssembliesXml != null)
                    return allAssembliesXml;

                allAssembliesXml = base.AllAssembliesXml;
                var assemblies = new List<ProductAssemblyXml>(allAssembliesXml.Assemblies)
                {
                    new ProductAssemblyXml
                    {
                        Configurations = "Common",
                        Name = typeof(Net46CompatibleFrameworkLocationHelper).Assembly.GetName().Name
                    }
                };
                allAssembliesXml.Assemblies = assemblies.ToArray();
                return allAssembliesXml;
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
    // Don't include this assembly, it's already been added as part of AllAssembliesXml above
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
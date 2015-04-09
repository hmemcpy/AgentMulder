﻿using System;
﻿using System.Collections.Generic;
﻿using JetBrains.Application;
﻿using JetBrains.Application.BuildScript.Application;
﻿using JetBrains.Application.BuildScript.Application.Zones;
﻿using JetBrains.Application.BuildScript.PackageSpecification;
﻿using JetBrains.Application.BuildScript.Solution;
﻿using JetBrains.Application.Environment;
﻿using JetBrains.Application.Environment.HostParameters;
﻿using JetBrains.Metadata.Reader.API;
﻿using JetBrains.Metadata.Utils;
﻿using JetBrains.ReSharper.TestFramework;
﻿using JetBrains.TestFramework;
﻿using JetBrains.TestFramework.Application.Zones;
﻿using JetBrains.TestFramework.Utils;
﻿using JetBrains.Util;
﻿using NUnit.Framework;

[assembly: TestDataPathBase(@"Test\Data")]

[ZoneDefinition]
public class TestEnvironmentZone : ITestsZone, IRequire<PsiFeatureTestZone>
{ 
}

[SetUpFixture]
public class ReSharperTestEnvironmentAssembly : ExtensionTestEnvironmentAssembly<TestEnvironmentZone>
{
    protected override JetHostItems.Packages CreateJetHostPackages(JetHostItems.Engine engine)
    {
      var mainAssembly = GetType().Assembly;
      var productBinariesDir = mainAssembly.GetPath().Parent;

      // Home dir
      if (AllAssembliesLocator.TryGetProductHomeDirOnSources(productBinariesDir).IsNullOrEmpty())
      {
        var relativeTestDataPath = TestUtil.GetTestDataPathBase_Find_FromAttr(mainAssembly) ?? TestUtil.DefaultTestDataPath;
        var productHomeDir = FileSystemUtil.GetDirectoryNameOfItemAbove(productBinariesDir, relativeTestDataPath);
        Environment.SetEnvironmentVariable(AllAssembliesLocator.ProductHomeDirEnvironmentVariableName, productHomeDir.FullPath);
      }

      return engine.OnPackagesInFlatFolderDependentOnAssembly(mainAssembly,
        packages =>
        {
          var packageFiles = new HashSet<ApplicationPackageFile>(new JetBrains.EqualityComparer<ApplicationPackageFile>(
            (file1, file2) => file1.LocalInstallPath == file2.LocalInstallPath, file => file.LocalInstallPath.GetHashCode()));
          var packageReferences = new HashSet<ApplicationPackageReference>(new JetBrains.EqualityComparer<ApplicationPackageReference>(
            (reference1, reference2) => string.Equals(reference1.PackageId, reference2.PackageId, StringComparison.OrdinalIgnoreCase),
            reference => reference.PackageId.GetHashCode()));
          var assemblyNameInfo = AssemblyNameInfo.Parse(mainAssembly.FullName);
          using (var loader = new MetadataLoader(productBinariesDir))
          {
            ProcessAssembly(packages, productBinariesDir, loader, assemblyNameInfo, packageFiles, packageReferences);
          }
          var packageArtifact = new ApplicationPackageArtifact(new SubplatformName(assemblyNameInfo.Name), new JetSemanticVersion(assemblyNameInfo.Version),  CompanyInfo.Name, CompanyInfo.NameWithInc, DateTime.UtcNow, null, null, packageFiles, new JetPackageMetadata(), packageReferences, Guid.Empty);
          return new AllAssembliesOnPackages(packages.Subplatforms.Concat(new SubplatformOnPackage(packageArtifact)).AsCollection());
        });
    }

    private static void ProcessAssembly(AllAssemblies allAssemblies, FileSystemPath productBinariesDir, MetadataLoader metadataLoader, AssemblyNameInfo assemblyNameInfo, HashSet<ApplicationPackageFile> packageFiles, HashSet<ApplicationPackageReference> packageReferences)
    {
      var assembly = metadataLoader.TryLoad(assemblyNameInfo, JetFunc<AssemblyNameInfo>.False, false);
      if (assembly == null) return;

      var subplatformOfAssembly = allAssemblies.FindSubplatformOfAssembly(assemblyNameInfo, OnError.Ignore);

      if (subplatformOfAssembly != null)
      {
        var subplatformReference = new ApplicationPackageReference(subplatformOfAssembly.Name, subplatformOfAssembly.GetCompanyNameHuman());
        packageReferences.Add(subplatformReference);
        return;
      }

      if (!packageFiles.Add(new ApplicationPackageFile(assembly.Location.MakeRelativeTo(productBinariesDir), assemblyNameInfo)))
        return;

      foreach (var referencedAssembly in assembly.ReferencedAssembliesNames)
      {
        ProcessAssembly(allAssemblies, productBinariesDir, metadataLoader, referencedAssembly, packageFiles, packageReferences);
      }
    }
}
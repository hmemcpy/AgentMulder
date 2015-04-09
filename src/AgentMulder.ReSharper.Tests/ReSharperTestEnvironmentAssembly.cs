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
﻿using NUnit.Framework;


/// <summary>
/// Test environment. Must be in the global namespace.
/// </summary>
// ReSharper disable CheckNamespace

[ZoneDefinition]
// ReSharper disable once CheckNamespace
public class TestEnvironmentZone : ITestsZone, IRequire<PsiFeatureTestZone>
{ 
}

[SetUpFixture]
public class ReSharperTestEnvironmentAssembly : ExtensionTestEnvironmentAssembly<TestEnvironmentZone>
{

}
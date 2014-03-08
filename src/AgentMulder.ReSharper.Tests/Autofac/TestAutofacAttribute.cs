using System.Collections.Generic;
using Autofac;
using JetBrains.ReSharper.TestFramework;

namespace AgentMulder.ReSharper.Tests.Autofac
{
    public class TestAutofacAttribute : TestReferencesAttribute
    {

        public override IEnumerable<string> GetReferences(JetBrains.ProjectModel.PlatformID platformId)
        {
            return new[]
            {
                typeof(ContainerBuilder).Assembly.Location
            };
        }
    }
}
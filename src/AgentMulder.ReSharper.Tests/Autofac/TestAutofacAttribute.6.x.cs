using System.Collections.Generic;
using Autofac;
using JetBrains.ReSharper.TestFramework;

namespace AgentMulder.ReSharper.Tests.Autofac
{
    public partial class TestAutofacAttribute
    {
        public override string[] GetReferences()
        {
            return new[]
            {
                typeof(ContainerBuilder).Assembly.Location
            };
        }
    }
}
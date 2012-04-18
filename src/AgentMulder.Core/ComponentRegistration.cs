using System;
using AgentMulder.Core.Patterns;

namespace AgentMulder.Core
{
    public class ComponentRegistration : IComponentRegistration
    {
        private readonly string componentFullName;
        private readonly IServiceImplementor componentImplementor;

        public string ComponentFullName
        {
            get { return componentFullName; }
        }

        public IServiceImplementor ComponentImplementor
        {
            get { return componentImplementor; }
        }

        public ComponentRegistration(string componentFullName, IServiceImplementor componentImplementor)
        {
            this.componentFullName = componentFullName;
            this.componentImplementor = componentImplementor;
        }
    }

    public interface IServiceImplementor
    {
        string GetImplementation(Func<string> getImplementation);
    }
}
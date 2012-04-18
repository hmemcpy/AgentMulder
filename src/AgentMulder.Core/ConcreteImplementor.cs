using System;

namespace AgentMulder.Core
{
    public class ConcreteImplementor : IServiceImplementor
    {
        private readonly string implementorFullName;

        public ConcreteImplementor(string implementorFullName)
        {
            this.implementorFullName = implementorFullName;
        }

        public string GetImplementation(Func<string> getImplementation)
        {
            return implementorFullName;
        }
    }
}
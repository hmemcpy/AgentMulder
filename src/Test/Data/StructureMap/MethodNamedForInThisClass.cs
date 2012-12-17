// Patterns: 0

using TestApplication.Types;

namespace TestApplication.StructureMap
{
    public class MethodNamedForInThisClass
    {
        public MethodNamedForInThisClass()
        {
            For<ICommon>().Use<CommonImpl1>();
        }

        private Bindable For<T>()
        {
            return new Bindable();
        }
    }
}
using TestApplication.Types;

namespace TestApplication.StructureMap
{
    public class MethodNamedBindInThisClass
    {
        public MethodNamedBindInThisClass()
        {
            For<ICommon>().Use<CommonImpl1>();
        }

        private Bindable For<T>()
        {
            return new Bindable();
        }
    }
}
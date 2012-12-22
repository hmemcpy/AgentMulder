// Patterns: 0

using TestApplication.Types;

namespace TestApplication.Ninject
{
    public class MethodNamedBindInThisClass
    {
        public MethodNamedBindInThisClass()
        {
            Bind<ICommon>().To<CommonImpl1>();
        }

        private Bindable Bind<T>()
        {
            return new Bindable();
        }
    }
}
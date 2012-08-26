using Catel.IoC;
using TestApplication.Types;

namespace TestApplication.Catel
{
    public class RegisterTypeGeneric
    {
        public RegisterTypeGeneric()
        {
            ServiceLocator.Instance.RegisterType<ICommon, CommonImpl1>();
        } 
    }
}
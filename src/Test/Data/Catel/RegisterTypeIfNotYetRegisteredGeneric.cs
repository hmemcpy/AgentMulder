using Catel.IoC;
using TestApplication.Types;

namespace TestApplication.Catel
{
    public class RegisterTypeIfNotYetRegisteredGeneric
    {
        public RegisterTypeIfNotYetRegisteredGeneric()
        {
            ServiceLocator.Instance.RegisterTypeIfNotYetRegistered<ICommon, CommonImpl1>();
        } 
    }
}
using Catel.IoC;
using TestApplication.Types;

namespace TestApplication.Catel
{
    public class RegisterTypeIfNotYetRegisteredNonGeneric
    {
        public RegisterTypeIfNotYetRegisteredNonGeneric()
        {
            ServiceLocator.Instance.RegisterTypeIfNotYetRegistered(typeof(ICommon), typeof(CommonImpl1));
        } 
    }
}
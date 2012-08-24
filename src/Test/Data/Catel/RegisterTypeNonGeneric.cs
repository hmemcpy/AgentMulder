using Catel.IoC;
using TestApplication.Types;

namespace TestApplication.Catel
{
    public class RegisterTypeNonGeneric
    {
        public RegisterTypeNonGeneric()
        {
            ServiceLocator.Instance.RegisterType(typeof(ICommon), typeof(CommonImpl1));
        }
    }
}
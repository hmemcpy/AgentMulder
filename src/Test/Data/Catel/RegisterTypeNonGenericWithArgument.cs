using Catel.IoC;
using TestApplication.Types;

namespace TestApplication.Catel
{
    public class RegisterTypeNonGenericWithArgument
    {
        public RegisterTypeNonGenericWithArgument()
        {
            ServiceLocator.Instance.RegisterType(typeof(ICommon), typeof(CommonImpl1), false);
        }
    }
}
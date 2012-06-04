using Microsoft.Practices.Unity;
using TestApplication.Types;

namespace TestApplication.Unity
{
    public class RegisterTypeNonGeneric
    {
        public RegisterTypeNonGeneric()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(ICommon), typeof(CommonImpl1));
        }
    }
}
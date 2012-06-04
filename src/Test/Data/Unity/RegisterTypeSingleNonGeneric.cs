using Microsoft.Practices.Unity;
using TestApplication.Types;

namespace TestApplication.Unity
{
    public class RegisterTypeSingleNonGeneric
    {
        public RegisterTypeSingleNonGeneric()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(CommonImpl1));
        }
    }
}
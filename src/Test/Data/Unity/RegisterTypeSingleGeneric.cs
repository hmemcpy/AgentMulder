using Microsoft.Practices.Unity;
using TestApplication.Types;

namespace TestApplication.Unity
{
    public class RegisterTypeSingleGeneric
    {
        public RegisterTypeSingleGeneric()
        {
            var container = new UnityContainer();
            container.RegisterType<CommonImpl1>();
        }
    }
}